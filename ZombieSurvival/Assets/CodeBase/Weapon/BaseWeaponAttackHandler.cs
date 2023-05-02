using CodeBase.Logic.Pause;
using CodeBase.Player;
using CodeBase.Player.PlayerAnimation;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using UnityEngine;

namespace CodeBase.Weapon
{
    public abstract class BaseWeaponAttackHandler : MonoBehaviour, IFreeze
    {
        [field: SerializeField] public WeaponHitEffect WeaponHitEffect { get; private set; }
        protected WeaponAttackTimer Timer { get; private set; }
        protected PlayerAudioPlayer AudioPlayer { get; private set; }

        protected WeaponConfigContainer ConfigContainer { get; private set; }
        protected WeaponDataContainer WeaponDataContainer { get; private set; }
        protected Camera MainCamera { get; private set; }
        protected PlayerAnimator Animator { get; private set; }
        protected IInputService InputService { get; private set; }
        protected BaseWeaponConfig Config { get; private set; }
        protected bool IsPause { get; private set; }

        public void Construct(WeaponDataContainer weaponDataContainer, IStaticDataService dataService, IInputService inputService, Camera mainCamera, PlayerAnimator playerAnimator, PlayerAudioPlayer audioPlayer)
        {
            WeaponDataContainer = weaponDataContainer;
            ConfigContainer = dataService.WeaponConfigContainer;
            Config = dataService.ForWeapon(weaponDataContainer.GetData().WeaponId);
            MainCamera = mainCamera;
            Animator = playerAnimator;
            AudioPlayer = audioPlayer;
            InputService = inputService;
            Timer = new WeaponAttackTimer(Config.AttackDelay);
            Animator.OnEvent += AnimationEventReader;
        }

        private void OnDestroy()
        {
            Animator.OnEvent -= AnimationEventReader;
            OnDestroyed();
        }

        protected virtual void OnDestroyed()
        { }

        protected virtual void WeaponWereShow()
        { }

        protected virtual void OnAnimationEventReader(PlayerAnimationEventId id)
        { }

        private void AnimationEventReader(PlayerAnimationEventId id)
        {
            if (PlayerAnimationEventId.Weapon_WeaponWereShow == id)
                WeaponWereShow();
            OnAnimationEventReader(id);
        }

        #region Pause

        public void Pause()
        {
            OnPause();
        }

        public void Play()
        {
            OnPlay();
        }

        public void Freeze()
        {
            IsPause = true;
            OnFreeze();
        }

        public void Unfreeze()
        {
            IsPause = false;
            OnUnfreeze();
        }

        protected virtual void OnPause()
        {
        }

        protected virtual void OnPlay()

        {
        }

        protected virtual void OnFreeze()
        {
        }

        protected virtual void OnUnfreeze()
        {
        }

        #endregion Pause
    }
}