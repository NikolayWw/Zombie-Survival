using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class AppBuyButton : MonoBehaviour
    {
        private const string InitProduct = "initialize_product";
        private const string AlreadyHaveThisWeaponMessage = "you already have this weapon";
        private const string InventoryFullMessage = "Inventory is Full";
        private const string CanBuyButtonMessage = "AP Buy";

        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private CodelessIAPButton _iapButton;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Button _buyButton;

        private IStaticDataService _staticData;
        private WeaponSlotsHandler _slotsHandler;
        private WeaponId _currentId;

        public void Construct(IStaticDataService staticData, ILogicFactory logicFactory)
        {
            _staticData = staticData;
            _slotsHandler = logicFactory.WeaponSlotsHandler;

            LockButton();
            _shopWindow.OnWeaponRefresh += RefreshWeapon;
            _shopWindow.OnAidKitRefresh += RefreshAidKit;
        }

        public void OnPurchaseComplited(Product product)
        {
            _slotsHandler.Add(NewWeaponData(_currentId));
            _shopWindow.SendWeaponPurchased(_currentId);
        }

        private void RefreshWeapon(WeaponId id)
        {
            if (WeaponId.None == id)
            {
                LockButton();
                return;
            }
            else if (_slotsHandler.CanAdd(id) == false)
            {
                LockButton(InventoryFullMessage);
                return;
            }
            else if (_slotsHandler.IsWeaponPresentInInventory(id) && _slotsHandler.CanStack(id) == false)
            {
                LockButton(AlreadyHaveThisWeaponMessage);
                return;
            }

            UnlockButton(id);
        }

        private void RefreshAidKit()
        {
            LockButton();
        }

        private void UnlockButton(WeaponId id)
        {
            _currentId = id;
            _iapButton.productId = id.ToString().ToLower();
            _buyButton.interactable = true;
            _buttonText.text = $"{CanBuyButtonMessage} {_staticData.ForWeapon(id).InAppPrice}";
        }

        private void LockButton(in string buttonText = "")
        {
            _buyButton.interactable = false;
            _iapButton.productId = InitProduct;
            _currentId = WeaponId.None;
            _buttonText.text = buttonText;
        }

        private WeaponDataContainer NewWeaponData(WeaponId weaponId)
        {
            switch (_staticData.ForWeapon(weaponId))
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