using CodeBase.Data;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _buttonText;

        private IStaticDataService _dataService;
        private WeaponSlotsHandler _slotsHandler;
        private PlayerData _playerData;

        private WeaponId _currentId;
        private BuyItemCategoryId _buyCategoryId;

        private enum BuyItemCategoryId
        {
            None,
            Weapon = 1,
            FirstAidKit = 2,
        }

        public void Construct(IStaticDataService dataService, ILogicFactory logicFactory, IPersistentProgressService persistentProgressService)
        {
            _dataService = dataService;
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _playerData = persistentProgressService.PlayerProgress.PlayerData;

            _buyButton.onClick.AddListener(Buy);
            _shopWindow.OnWeaponRefresh += RefreshWeapon;
            _shopWindow.OnAidKitRefresh += RefreshFirstAidKit;
            ResetData();
        }

        private void RefreshWeapon(WeaponId id)
        {
            ResetData();
            _buyCategoryId = BuyItemCategoryId.Weapon;
            _currentId = id;

            if (_playerData.Money <= GetConfig().Price)
            {
                _buttonText.text = "not enough money";
                _buyButton.interactable = false;
                return;
            }
            if (_slotsHandler.IsInventoryFull())
            {
                _buttonText.text = "no inventory space";
                _buyButton.interactable = false;
                return;
            }
            if (_slotsHandler.IsWeaponPresentInInventory(_currentId) && _slotsHandler.CanStack(_currentId) == false)
            {
                _buttonText.text = "you already have this weapon";
                _buyButton.interactable = false;
                return;
            }
            _buttonText.text = "Buy";
            _buyButton.interactable = true;
        }

        private void RefreshFirstAidKit()
        {
            ResetData();
            _buyCategoryId = BuyItemCategoryId.FirstAidKit;

            if (_playerData.Money < _dataService.AidKitConfig.Price)
                return;

            _buttonText.text = "Buy";
            _buyButton.interactable = true;
        }

        private void Buy()
        {
            switch (_buyCategoryId)
            {
                case BuyItemCategoryId.None:
                    break;

                case BuyItemCategoryId.Weapon:
                    _playerData.DecrementMoney(GetConfig().Price);
                    _slotsHandler.Add(NewWeaponData());
                    _shopWindow.SendWeaponPurchased(_currentId);
                    AnalyticsService.Instance.CustomData(AnalyticsConstants.BuyWeaponEvent, new Dictionary<string, object> { { AnalyticsConstants.WeaponNameParameter, _currentId.ToString() } });
                    break;

                case BuyItemCategoryId.FirstAidKit:
                    _playerData.DecrementMoney(_dataService.AidKitConfig.Price);
                    _playerData.IncrementAidKit(_dataService.AidKitConfig.BuyAmount);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ResetData()
        {
            _buyCategoryId = BuyItemCategoryId.None;
            _currentId = WeaponId.None;
            _buttonText.text = string.Empty;
            _buyButton.interactable = false;
        }

        private BaseWeaponConfig GetConfig() =>
            _dataService.ForWeapon(_currentId);

        private WeaponDataContainer NewWeaponData()
        {
            switch (GetConfig())
            {
                case FirearmsConfig data:
                    return new WeaponDataContainer(new FirearmWeaponData(_currentId, 0, data.BuyAmount));

                case MeleeWeaponConfig _:
                    return new WeaponDataContainer(new MeleeWeaponData(_currentId));
            }

            return null;
        }
    }
}