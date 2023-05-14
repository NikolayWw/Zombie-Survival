using CodeBase.Player;
using CodeBase.Player.PlayerAnimation;
using UnityEngine;

namespace CodeBase.Weapon.Firearms
{
    public class FirearmsWeaponAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _magazineParent;
        [SerializeField] private Transform _magazine;

        private PlayerAnimator _playerAnimator;
        private PlayerAnchors _playerAnchors;

        private Vector3 _startMagazinePosition;
        private Quaternion _startMagazineRotate;

        public void Construct(PlayerAnchors playerAnchors, PlayerAnimator playerAnimator)
        {
            _playerAnchors = playerAnchors;
            _playerAnimator = playerAnimator;
            _playerAnimator.OnEvent += ReadEvent;
        }

        private void OnDestroy()
        {
            _playerAnimator.OnEvent -= ReadEvent;

            if (_magazine.gameObject)
                Destroy(_magazine.gameObject);
        }

        private void ReadEvent(PlayerAnimationEventId eventId)
        {
            switch (eventId)
            {
                case PlayerAnimationEventId.Weapon_StartReload:
                    StartReload();
                    break;

                case PlayerAnimationEventId.Weapon_Reload:
                    EndReload();
                    break;
            }
        }

        private void StartReload()
        {
            _startMagazinePosition = _magazine.localPosition;
            _startMagazineRotate = _magazine.localRotation;
            _magazine.SetParent(_playerAnchors.ReloadMagazineAnchor);
        }

        private void EndReload()
        {
            _magazine.SetParent(_magazineParent);
            _magazine.localPosition = _startMagazinePosition;
            _magazine.localRotation = _startMagazineRotate;
        }
    }
}