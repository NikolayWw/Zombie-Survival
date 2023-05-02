using CodeBase.Data.WeaponInventory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.SlotHandlers
{
    public class UIWeaponSlotUpdate : MonoBehaviour
    {
        [field: SerializeField] public Image SelectableImage { get; private set; }
        [field: SerializeField] public Image IconImage { get; private set; }

        private WeaponSlotsContainer _inventorySlots;
        private WeaponSlot _slot;
        private IStaticDataService _dataService;
        private int _slotIndex;

        public void Construct(IPersistentProgressService persistentProgressService, IStaticDataService dataService, int slotIndex)
        {
            _inventorySlots = persistentProgressService.PlayerProgress.WeaponSlotsContainer;
            _dataService = dataService;
            _slotIndex = slotIndex;

            _slot = _inventorySlots.Slots[_slotIndex];
            _slot.OnSlotChangeValue += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            _slot.OnSlotChangeValue -= Refresh;
        }

        private void Refresh()
        {
            IconImage.sprite = WeaponPresent() ? _dataService.ForWeapon(_slot.WeaponDataContainer.GetData().WeaponId).Icon : _dataService.EmptySprite;
            SelectableImage.sprite = _slot.IsSelected() ? _dataService.InventoryWeaponConfig.SlotSelectedSprite : _dataService.EmptySprite;
        }

        private bool WeaponPresent() =>
            _slot?.IsEmpty() == false && _slot.WeaponDataContainer?.GetData().WeaponId != WeaponId.None;
    }
}