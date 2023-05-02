using CodeBase.Logic.ApplyDamage;
using CodeBase.Player;
using CodeBase.Player.PlayerAnimation;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using UnityEngine;

namespace CodeBase.Weapon
{
    public class MeleeWeaponAttackHandler : BaseWeaponAttackHandler
    {
        private MeleeWeaponConfig _config;

        public void Construct()
        {
            _config = Config as MeleeWeaponConfig;
        }

        private void Start()
        {
            AudioPlayer.Play(_config.WeaponWhereShow);
        }

        private void Update()
        {
            if (IsPause)
                return;

            Fire();
        }

        protected override void OnAnimationEventReader(PlayerAnimationEventId id)
        {
            switch (id)
            {
                case PlayerAnimationEventId.Weapon_Fire:
                    FireProcess();
                    break;
            }
        }

        protected override void OnFreeze()
        {
            Animator.StopPlayFire();
        }

        private void Fire()
        {
            if (InputService.IsFirePressed)
                Animator.PlayFire();
            else
                Animator.StopPlayFire();
        }

        private void FireProcess()
        {
            AudioPlayer.Play(_config.AttackAudio);
            Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _config.AttackDistance, ConfigContainer.ApplyDamageLayer))
                if (hit.collider.TryGetComponent(out IApplyDamage damage))
                {
                    WeaponHitEffect.Play(damage, hit);
                    damage.ApplyDamage(_config.Damage, ray.direction * _config.HitPushForce);
                }
        }
    }
}