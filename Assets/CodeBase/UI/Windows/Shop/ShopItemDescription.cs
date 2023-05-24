using CodeBase.Data.WeaponInventory;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemDescription : MonoBehaviour
    {
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _ammoText;
        [SerializeField] private TMP_Text _weaponTypeText;
        [SerializeField] private Image _iconImage;

        private IStaticDataService _dataService;
        private WeaponSlotsHandler _slotsHandler;
        private WeaponId _currentId;
        private WeaponSlotsContainer _inventory;

        public void Construct(IStaticDataService dataService, ILogicFactory logicFactory, IPersistentProgressService persistentProgressService)
        {
            _dataService = dataService;
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _inventory = persistentProgressService.PlayerProgress.WeaponSlotsContainer;

            Subscribe(true);
        }

        private void Start()
        {
            Hide();
        }

        private void OnDestroy()
        {
            Subscribe(false);
        }

        private void RefreshWeapon(WeaponId id)
        {
            _currentId = id;
            if (WeaponId.None == id)
            {
                Hide();
                return;
            }

            Show();
            BaseWeaponConfig config = _dataService.ForWeapon(id);
            _iconImage.sprite = config.Icon;
            _priceText.text = $"Price: {config.Price}";
            _nameText.text = config.Name;

            RefreshAmmo();

            switch (config)
            {
                case FirearmsConfig _:
                    _weaponTypeText.text = "Firearm";
                    break;

                case MeleeWeaponConfig _:
                    _weaponTypeText.text = "Melee Weapon";
                    break;
            }
        }

        private void RefreshAidKit()
        {
            Show();
            CleanUI();
            _nameText.text = _dataService.AidKitConfig.Name;
            _priceText.text = $"Price: {_dataService.AidKitConfig.Price}";
            _iconImage.sprite = _dataService.AidKitConfig.Icon;

            _ammoText.text = string.Empty;
            _weaponTypeText.text = string.Empty;
        }

        private void RefreshAmmo()
        {
            if (_slotsHandler.IsWeaponPresentInInventory(_currentId, out WeaponSlot slot))
            {
                switch (slot.WeaponDataContainer.GetData())
                {
                    case FirearmWeaponData data:
                        _ammoText.text = $"Ammo: {data.Amount}";
                        break;

                    default:
                        _ammoText.text = string.Empty;
                        break;
                }
            }
            else
            {
                _ammoText.text = string.Empty;
            }
        }

        private void Subscribe(bool isSubscribe)
        {
            foreach (var slot in _inventory.Slots)
            {
                if (isSubscribe)
                {
                    _shopWindow.OnWeaponRefresh += RefreshWeapon;
                    _shopWindow.OnAidKitRefresh += RefreshAidKit;
                    slot.OnSlotChangeValue += RefreshAmmo;
                }
                else
                {
                    _shopWindow.OnWeaponRefresh -= RefreshWeapon;
                    _shopWindow.OnAidKitRefresh -= RefreshAidKit;
                    slot.OnSlotChangeValue -= RefreshAmmo;
                }
            }
        }

        private void Show() =>
            gameObject.SetActive(true);

        private void Hide() =>
            gameObject.SetActive(false);

        private void CleanUI()
        {
            _nameText.text = string.Empty;
            _priceText.text = string.Empty;
            _ammoText.text = string.Empty;
            _weaponTypeText.text = string.Empty;
            _iconImage.sprite = _dataService.EmptySprite;
        }
    }
}