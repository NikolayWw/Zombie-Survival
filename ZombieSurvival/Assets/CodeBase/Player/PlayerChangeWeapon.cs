using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Player.PlayerAnimation;
using CodeBase.Services.Factory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.UI.Services.Window;
using CodeBase.Weapon;
using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerChangeWeapon : MonoBehaviour, ISaveLoad
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerAudioPlayer _playerAudioPlayer;
        [SerializeField] private PlayerAnchors _playerAnchors;
        [SerializeField] private Transform _weaponAnhour;
        public bool CanSave => true;

        private IGameFactory _gameFactory;
        private WeaponSlotsHandler _slotsHandler;
        private IWindowService _windowService;
        public BaseWeaponAttackHandler CurrentWeapon { get; private set; }
        private bool _weaponInHandPresent => CurrentWeapon != null;

        public void Construct(IGameFactory gameFactory, IWindowService windowService, ILogicFactory logicFactory)
        {
            _gameFactory = gameFactory;
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _windowService = windowService;

            Subscribe(true);
        }

        private void OnDestroy() =>
            Subscribe(false);

        public void Save()
        { }

        public void Load()
        {
            if (GetWeaponCondition())
                CreateWeapon();
        }

        private void SlotChangeValue(UpdateWeaponSlotType type)
        {
            switch (type)
            {
                case UpdateWeaponSlotType.Drop:
                    if (_weaponInHandPresent)
                        ClearCurrentWeapon();
                    break;

                case UpdateWeaponSlotType.AddItem:
                    if (GetWeaponCondition())
                        CreateWeapon();
                    break;

                case UpdateWeaponSlotType.SelectSlot:
                    ChangeWeapon();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void ChangeWeapon()
        {
            if (_weaponInHandPresent)
                ClearCurrentWeapon();

            if (GetWeaponCondition())
                CreateWeapon();
        }

        private bool GetWeaponCondition() =>
            _slotsHandler.IsCurrentSelectAndWeaponPresent(out _) && _weaponInHandPresent == false;

        private void CreateWeapon()
        {
            _slotsHandler.IsCurrentSelectAndWeaponPresent(out var slot);
            WeaponDataContainer data = slot.WeaponDataContainer;
            CurrentWeapon = _gameFactory.CreateWeaponInHand(data, _weaponAnhour, _animator, _playerAudioPlayer, _playerAnchors, _windowService);
            _windowService.OpenUpdateWeaponWindow(data);
            _animator.PlayWeaponIdle(data.GetData().WeaponId);
        }

        private void Subscribe(bool isSubscribe)
        {
            if (isSubscribe)
            {
                _slotsHandler.OnSelectedSlotHaveBeenUpdated += SlotChangeValue;
            }
            else
            {
                _slotsHandler.OnSelectedSlotHaveBeenUpdated -= SlotChangeValue;
            }
        }

        private void ClearCurrentWeapon()
        {
            _animator.ResetWeaponParameters();
            _animator.PlayIdle();

            if (CurrentWeapon)
            {
                _windowService.CloseUpdateWeapon();
                Destroy(CurrentWeapon.gameObject);
                CurrentWeapon = null;
            }
        }
    }
}