using CodeBase.StaticData.Inventory;
using System;
using UnityEngine;

namespace CodeBase.Data.WeaponInventory
{
    [Serializable]
    public class WeaponSlotsContainer
    {
        [field: SerializeField] public WeaponSlot[] Slots { get; private set; }

        public WeaponSlotsContainer(InventoryWeaponConfig inventoryWeaponConfig)
        {
            Slots = new WeaponSlot[inventoryWeaponConfig.SlotCount];
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new WeaponSlot(i);
            }
        }
    }
}