using CodeBase.Services.Factory;
using CodeBase.StaticData.Items.WeaponItems.GravityGun;
using UnityEngine;

namespace CodeBase.Weapon.GravityGun
{
    public class GravityGunAttackHandler : BaseWeaponAttackHandler
    {
        private GravityGunObject _activeGravity;
        private IGameFactory _gameFactory;
        private GravityGunAnchor _gravityGunAnchor;
        private GravityGunConfig _config;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _config = Config as GravityGunConfig;
        }

        private void Start()
        {
            AudioPlayer.Play(_config.WeaponWhereShow);
        }

        private void Update()
        {
            if (IsPause)
                return;

            UpdateGravityFire();
        }

        private void UpdateGravityFire()
        {
            if (InputService.IsFirePressed)
            {
                if (_activeGravity == null)
                {
                    if (FindGravityObject())
                        StartGravityGun();
                }
            }
            else
            {
                StopGravity();
            }
        }

        private bool FindGravityObject()
        {
            Ray ray = MainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _activeGravity = hit.transform.GetComponent<GravityGunObject>();
                return _activeGravity != null;
            }

            _activeGravity = null;
            return false;
        }

        private void StartGravityGun()
        {
            _gravityGunAnchor = _gameFactory.CreateGravityGunAnchor();
            _activeGravity.StartGravityGun(_gravityGunAnchor.Rigidbody, _gravityGunAnchor.transform);
            _activeGravity.OnEndGravity += StopGravity;
        }

        private void StopGravity()
        {
            if (_activeGravity)
            {
                _activeGravity.OnEndGravity -= StopGravity;
                _activeGravity.StopGravityGun();
            }

            _gravityGunAnchor?.Remove();

            _gravityGunAnchor = null;
            _activeGravity = null;
        }

        protected override void OnDestroyed()
        {
            _activeGravity?.StopGravityGun();
        }
    }
}