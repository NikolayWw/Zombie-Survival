using UnityEngine;

namespace CodeBase.StaticData.Inventory
{
    [CreateAssetMenu(fileName = "New InventoryWeaponData", menuName = "Static Data/Inventory/InventoryWeaponData", order = 0)]
    public class InventoryWeaponConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite SlotSelectedSprite { get; private set; }

        [field: SerializeField] public int SlotCount { get; private set; } = 1;
        [field: SerializeField] public float DropDistance { get; private set; }

        private void OnValidate()
        {
            if (SlotCount < 1) SlotCount = 1;
        }
    }
}