using CodeBase.Logic.Pause;
using CodeBase.StaticData.Items.WeaponItems;
using System;
using UnityEngine;

namespace CodeBase.Player.PlayerAnimation
{
    public class PlayerAnimator : MonoBehaviour, IPause
    {
        private readonly int IdleHash = Animator.StringToHash("Idle");
        private readonly int WeaponReloadHash = Animator.StringToHash("Reload");
        private readonly int FireHash = Animator.StringToHash("Fire");
        private readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");
        private readonly int WeaponPresentHash = Animator.StringToHash("WeaponPresent");

        [SerializeField] private Animator _animator;
        public bool IsReloading => _animator.GetBool(WeaponReloadHash);

        public Action<PlayerAnimationEventId> OnEvent;
        private float _pauseAnimationSpeed;

        public void PlayWeaponIdle(WeaponId id)
        {
            _animator.Play(IdleHash, 0, 0);
            switch (id)
            {
                case WeaponId.None:
                    break;

                case WeaponId.Melee_Machete:
                case WeaponId.Melee_Sword:
                    _animator.SetInteger(WeaponTypeHash, (int)WeaponId.Melee_Sword);
                    break;

                case WeaponId.Firearms_PistolSmall:
                case WeaponId.Firearms_Pistol:
                    _animator.SetInteger(WeaponTypeHash, (int)WeaponId.Firearms_Pistol);
                    break;

                case WeaponId.Firearms_M16:
                case WeaponId.Firearms_BanditGun:
                case WeaponId.GravityGun_Gun:
                    _animator.SetInteger(WeaponTypeHash, (int)WeaponId.Firearms_M16);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }

            _animator.SetBool(WeaponPresentHash, WeaponId.None != id);
        }

        public void PlayIdle() =>
            _animator.Play(IdleHash, 0, 0);

        public void PlayFire() =>
            _animator.SetBool(FireHash, true);

        public void StopPlayFire() =>
            _animator.SetBool(FireHash, false);

        public void PlayReload() =>
            _animator.SetBool(WeaponReloadHash, true);

        public void ResetWeaponParameters()
        {
            _animator.SetBool(WeaponPresentHash, false);
            _animator.SetBool(WeaponReloadHash, false);
            _animator.SetBool(FireHash, false);
        }

        public void Play()
        {
            _animator.speed = _pauseAnimationSpeed;
        }

        public void Pause()
        {
            _pauseAnimationSpeed = _animator.speed;
            _animator.speed = 0;
        }

        private void AnimationEventRead(PlayerAnimationEventId id)//use event
        {
            switch (id)
            {
                case PlayerAnimationEventId.Weapon_Reload:
                    _animator.SetBool(WeaponReloadHash, false);
                    break;
            }
            OnEvent?.Invoke(id);
        }
    }
}