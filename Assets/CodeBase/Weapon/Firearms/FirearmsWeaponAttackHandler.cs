using CodeBase.Logic.ApplyDamage;
using CodeBase.Player;
using CodeBase.Player.PlayerAnimation;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using System;
using UnityEngine;

namespace CodeBase.Weapon.Firearms
{
    public class FirearmsWeaponAttackHandler : BaseWeaponAttackHandler
    {
        [SerializeField] private ParticleSystem _shotEffect;
        [SerializeField] private ShotSpread _spread;

        private FirearmsConfig _config;
        private FirearmWeaponData _weaponData;
        private Crosshair _crosshair;
        private Action _fireUpdate = Null;

        private bool _singleShootToggle;

        public void Construct()
        {
            _config = Config as FirearmsConfig;
            _weaponData = WeaponDataContainer.GetData() as FirearmWeaponData;
            _crosshair = new Crosshair();

            _spread.Initialize(MainCamera);

            InputService.OnWeaponReload += TryReload;
            _crosshair.ResetSettings(_config.CrosshairConfig);
        }

        private void Start()
        {
            AudioPlayer.Play(_config.WeaponWhereShow);
        }

        private void OnGUI()
        {
            _crosshair.OnGUI();
        }

        private void Update()
        {
            _spread.Update();
            _crosshair.UpdateSize(_spread.AccuracySize);
            _fireUpdate.Invoke();
        }

        protected override void OnFreeze()
        {
            _fireUpdate = Null;
        }

        protected override void OnUnfreeze()
        {
            ChangeWeaponShootType(_config.ShootType, _config.CrosshairConfig);
        }

        protected override void OnDestroyed()
        {
            InputService.OnWeaponReload -= TryReload;
        }

        protected override void WeaponWereShow()
        {
            ChangeWeaponShootType(_config.ShootType, _config.CrosshairConfig);
        }

        protected override void OnAnimationEventReader(PlayerAnimationEventId id)
        {
            switch (id)
            {
                case PlayerAnimationEventId.Weapon_Reload:
                    Reload();
                    break;
            }
        }

        private void ChangeWeaponShootType(FirearmWeaponShootType type, CrosshairConfig crosshairConfig)
        {
            switch (type)
            {
                case FirearmWeaponShootType.None:
                    _fireUpdate = Null;
                    break;

                case FirearmWeaponShootType.Single:
                    _crosshair.ResetSettings(crosshairConfig);
                    _fireUpdate = SingleShoot;
                    break;

                case FirearmWeaponShootType.Automatic:
                    _crosshair.ResetSettings(crosshairConfig);
                    _fireUpdate = AutomaticShoot;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TryReload()
        {
            if (IsPause)
                return;

            if (_weaponData.Amount <= 0 || _weaponData.Magazine >= _config.MaxMagazine)
                return;

            Animator.PlayReload();
            AudioPlayer.Play(_config.ReloadAudio);
        }

        private void Reload()
        {
            if (_weaponData.Amount + _weaponData.Magazine <= _config.MaxMagazine)
            {
                _weaponData.SetAmmoValue(_weaponData.Amount + _weaponData.Magazine, 0);
            }
            else
            {
                int amount = _weaponData.Amount + _weaponData.Magazine;
                amount -= _config.MaxMagazine;
                _weaponData.SetAmmoValue(_config.MaxMagazine, amount);
            }
        }

        private void FireProcess()
        {
            Animator.PlayFire();
            AudioPlayer.Play(_config.FireAudio);
            Timer.Start();
            _shotEffect.Play();
            _weaponData.DecreaseMagazine();
            _spread.Spread();

            Ray ray = new Ray(MainCamera.transform.position, _spread.Direction);
            if (Physics.Raycast(ray, out RaycastHit hit, _config.AttackDistance, ConfigContainer.ApplyDamageLayer))
                if (hit.collider.TryGetComponent(out IApplyDamage damage))
                {
                    WeaponHitEffect.Play(damage, hit);
                    damage.ApplyDamage(_config.Damage, ray.direction * _config.HitPushForce);
                }
        }

        private void SingleShoot()
        {
            if (ShootCondition() && _singleShootToggle)
            {
                if (_weaponData.Magazine > 0)
                    FireProcess();
                else
                    AudioPlayer.Play(_config.EmptyFireAudio);

                _singleShootToggle = false;
            }
            else if (_singleShootToggle == false && InputService.IsFirePressed == false)
            {
                Animator.StopPlayFire();
                _singleShootToggle = true;
            }
        }

        private void AutomaticShoot()
        {
            if (ShootCondition())
            {
                if (_weaponData.Magazine > 0)
                {
                    FireProcess();
                }
                else if (_singleShootToggle)
                {
                    AudioPlayer.Play(_config.EmptyFireAudio);
                    _singleShootToggle = false;
                }
            }
            else
            {
                Animator.StopPlayFire();
                _singleShootToggle = true;
            }
        }

        private bool ShootCondition()
        {
            return Timer.Elapsed
                   && IsPause == false
                   && InputService.IsFirePressed
                   && Animator.IsReloading == false;
        }

        private static void Null()
        { }
    }
}