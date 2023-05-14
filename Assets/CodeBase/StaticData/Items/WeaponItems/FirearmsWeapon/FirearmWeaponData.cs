using CodeBase.Data.WeaponInventory;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon
{
    [Serializable]
    public class FirearmWeaponData : BaseWeaponData
    {
        [field: SerializeField] public int Magazine { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        public FirearmWeaponData(WeaponId weaponId, int magazine, int amount) : base(weaponId)
        {
            if (WeaponDataExtension.IsFirearmsWeapon(weaponId) == false)
            {
                Debug.LogError("Incorrect weapon id");
                return;
            }

            Magazine = magazine;
            Amount = amount;

            CheckIfSettingsAreCorrect();
        }

        public void DecreaseMagazine()
        {
            Magazine--;
            CheckIfSettingsAreCorrect();
            OnChangeValue.Invoke();
        }

        public void SetAmmoValue(int magazine, int amount)
        {
            Magazine = magazine;
            Amount = amount;
            CheckIfSettingsAreCorrect();
            OnChangeValue?.Invoke();
        }

        private void CheckIfSettingsAreCorrect()
        {
            if (Amount < 0 || Magazine < 0)
            {
                Debug.LogError("incorrect firearm settings");
                if (Amount < 0) Amount = 0;
                if (Magazine < 0) Magazine = 0;
            }
        }
    }
}