using CodeBase.Data.WeaponInventory;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory
{
    public class UIWeaponSlotsContainer : MonoBehaviour
    {
        [SerializeField] private Transform _slotsWindow;

        private WeaponSlotsContainer _slotsContainer;

        public void Construct(IUIFactory uiFactory, IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _slotsContainer = persistentProgressService.PlayerProgress.WeaponSlotsContainer;
            InitSlots(uiFactory, windowService);
        }

        private void InitSlots(IUIFactory uiFactory, IWindowService windowService)
        {
            for (int i = 0; i < _slotsContainer.Slots.Length; i++)
            {
                uiFactory.CreateWeaponSlot(windowService, _slotsWindow, i);
            }
        }
    }
}