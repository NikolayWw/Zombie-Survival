using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Shop;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private ShopItemDescription _itemDescription;
        [SerializeField] private RewardedAddMoney _addMoney;

        [SerializeField] private GridLayoutGroup _contentGridLayoutGroup;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private Transform _slotsRoot;

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
            shopSlot.Initialize(_dataService.AidKitConfig.Name, _dataService.AidKitConfig.Icon, RefreshAidKit);
        }

        private void InitWeaponSlot(ShopConfig config)
        {
            UIShopSlot slot = _uiFactory.CreateShopSlot(_slotsRoot);
            BaseWeaponConfig weaponConfig = _dataService.ForWeapon(config.WeaponId);
            slot.Initialize(weaponConfig.Name, weaponConfig.Icon, () => RefreshWeapon(weaponConfig.WeaponId));
        }

        private void ControlContentSize()
        {
            const float numberOfColumns = 2f;

            int cellCount = Mathf.CeilToInt(_slotsCount / numberOfColumns);
            float height = cellCount * (_contentGridLayoutGroup.cellSize.y + _contentGridLayoutGroup.spacing.y);
            _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, height);
        }

        private void RefreshWeapon(WeaponId id)
        {
            _itemDescription.RefreshWeapon(id);
            _buyButton.RefreshWeapon(id);
        }

        private void RefreshAidKit()
        {
            _itemDescription.RefreshAidKit();
            _buyButton.RefreshFirstAidKit();
        }
    }
}