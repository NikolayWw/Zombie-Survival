using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Weapon
{
    public class UpdateWeaponAmmoWindow : BaseWindow
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _magazineText;
        [SerializeField] private TMP_Text _ammoText;

        private FirearmWeaponData _firearmsWeaponData;

        public void Construct(WeaponDataContainer weaponDataContainer, IStaticDataService dataService)
        {
            switch (weaponDataContainer.GetData())
            {
                case FirearmWeaponData data:
                    _firearmsWeaponData = data;
                    _iconImage.sprite = dataService.ForWeapon(data.WeaponId).Icon;
                    _firearmsWeaponData.OnChangeValue += FirearmsRefresh;
                    FirearmsRefresh();
                    break;

                case MeleeWeaponData _:
                    MeleeRefresh();
                    break;
            }
        }

        private void OnDestroy()
        {
            UnSubscriber();
        }

        private void MeleeRefresh()
        {
            _magazineText.text = string.Empty;
            _ammoText.text = string.Empty;
        }

        private void FirearmsRefresh()
        {
            _magazineText.text = _firearmsWeaponData.Magazine.ToString();
            _ammoText.text = _firearmsWeaponData.Amount.ToString();
        }

        private void UnSubscriber()
        {
            if (_firearmsWeaponData != null)
                _firearmsWeaponData.OnChangeValue -= FirearmsRefresh;
        }
    }
}