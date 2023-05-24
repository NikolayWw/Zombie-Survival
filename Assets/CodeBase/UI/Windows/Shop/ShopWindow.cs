using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Shop;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private RewardedAddMoney _addMoney;

        [SerializeField] private GridLayoutGroup _contentGridLayoutGroup;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private Transform _slotsRoot;

        public Action<WeaponId> OnWeaponRefresh;
        public Action OnAidKitRefresh;

        private IUIFactory _uiFactory;
        private IStaticDataService _dataService;
        private int _slotsCount;

        public void Construct(IUIFactory uiFactory, IStaticDataService dataService, IPersistentProgressService persistentProgressService, IAdsService adsService)
        {
            _uiFactory = uiFactory;
            _dataService = dataService;

            _addMoney.Construct(adsService, persistentProgressService);
        }

        public void Initialize(List<ShopConfig> shopConfigs)
        {
            InitSlots(shopConfigs);
            _addMoney.Initialize();
            ControlContentSize();
        }

        public void SendWeaponPurchased(WeaponId weaponId)
        {
            OnWeaponRefresh?.Invoke(weaponId);
        }

        protected override void OnClose()
        {
            base.OnClose();
            _addMoney.Clean();
        }

        private void InitSlots(List<ShopConfig> shopConfigs)
        {
            foreach (var config in shopConfigs)
            {
                if (config.IsAidKit)
                    InitAidKitSlot();
                else
                    InitWeaponSlot(config);
                _slotsCount++;
            }
        }

        private void InitAidKitSlot()
        {
            UIShopSlot shopSlot = _uiFactory.CreateShopSlot(_slotsRoot);
            shopSlot.Initialize(_dataService.AidKitConfig.Name, _dataService.AidKitConfig.Icon, OnAidKitRefresh);
        }

        private void InitWeaponSlot(ShopConfig config)
        {
            UIShopSlot slot = _uiFactory.CreateShopSlot(_slotsRoot);
            BaseWeaponConfig weaponConfig = _dataService.ForWeapon(config.WeaponId);
            slot.Initialize(weaponConfig.Name, weaponConfig.Icon, () => OnWeaponRefresh?.Invoke(weaponConfig.WeaponId));
        }

        private void ControlContentSize()
        {
            const float numberOfColumns = 2f;

            int cellCount = Mathf.CeilToInt(_slotsCount / numberOfColumns);
            float height = cellCount * (_contentGridLayoutGroup.cellSize.y + _contentGridLayoutGroup.spacing.y);
            _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, height);
        }
    }
}