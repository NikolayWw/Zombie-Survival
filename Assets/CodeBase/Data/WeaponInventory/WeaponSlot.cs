using CodeBase.StaticData.Items.WeaponItems;
using System;
using UnityEngine;

namespace CodeBase.Data.WeaponInventory
{
    [Serializable]
    public class WeaponSlot
    {
        [field: SerializeField] public WeaponDataContainer WeaponDataContainer { get; private set; }
        [field: SerializeField] public int SlotIndex { get; private set; }

        [SerializeField] private bool _isSelected;

        public Action OnSlotChangeValue;

        public Action<int> OnSelected;

        public WeaponSlot(int index)
        {
            SlotIndex = index;
        }

        public void SetWeapon(WeaponDataContainer weaponDataContainer)
        {
            WeaponDataContainer = weaponDataContainer;
            if (WeaponDataContainer != null)
                WeaponDataContainer.GetData().OnChangeValue += SendChangeValue;

            SendChangeValue();
        }

        public void Clear()
        {
            WeaponDataContainer = null;
            SendChangeValue();
        }

        public void SetSelected()
        {
            _isSelected = true;
            SendSelected();
            SendChangeValue();
        }

        public void DeSelected()
        {
            _isSelected = false;
            SendSelected();
            SendChangeValue();
        }

        public bool IsSelected() =>
            _isSelected;

        public bool IsEmpty() =>
            WeaponDataContainer?.GetData() == null || WeaponDataContainer.GetData().WeaponId == WeaponId.None;

        private void SendSelected() =>
            OnSelected?.Invoke(SlotIndex);

        private void SendChangeValue() =>
            OnSlotChangeValue?.Invoke();
    }
}