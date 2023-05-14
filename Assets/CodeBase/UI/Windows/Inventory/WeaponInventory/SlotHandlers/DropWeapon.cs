using CodeBase.Data.WeaponInventory;
using CodeBase.Infrastructure.Logic;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Logic.Pause;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.Inventory.WeaponInventory.Logic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.SlotHandlers
{
    public class DropWeapon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ICoroutineRunner, IFreeze
    {
        private UIWeaponTimer _dropUIWeaponTimer;
        private UIWeaponTimerUpdate _updateDropUIWeaponTimer;
        private WeaponSlotsContainer _slots;
        private IWindowService _windowService;
        private WeaponSlotsHandler _slotsHandler;
        private int _index;
        private bool _isPause;

        public void Construct(IPersistentProgressService persistentProgressService, IWindowService windowService, ILogicFactory logicFactory, int slotIndex)
        {
            _slots = persistentProgressService.PlayerProgress.WeaponSlotsContainer;
            _windowService = windowService;
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _index = slotIndex;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPause)
                return;

            if (_slots.Slots[_index].IsEmpty() == false)
            {
                _dropUIWeaponTimer = new UIWeaponTimer(this, 0.15f); //drop delay
                _dropUIWeaponTimer.OnElapsed += StartDropUpdate;
                _dropUIWeaponTimer.Start();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isPause)
                return;

            _dropUIWeaponTimer?.Unsubscribe();

            if (_updateDropUIWeaponTimer?.Elapsed == false)
            {
                _updateDropUIWeaponTimer.Unsubscribe();
                _windowService.Close(WindowId.DropWeaponWindow);
            }
        }

        private void StartDropUpdate()
        {
            _windowService.Open(WindowId.DropWeaponWindow);
            _windowService.GetWindow(WindowId.DropWeaponWindow, out DropWeaponWindow window);

            _updateDropUIWeaponTimer = new UIWeaponTimerUpdate(this, window.UpdateImage, ElapsedUpdateDropTimer);

            _updateDropUIWeaponTimer.Start();
        }

        private void ElapsedUpdateDropTimer()
        {
            if (_slots.Slots[_index].IsEmpty() == false)
                _slotsHandler.Drop(_index);
            _windowService.Close(WindowId.DropWeaponWindow);
        }

        #region Pause

        public void Pause()
        { }

        public void Play()
        { }

        public void Freeze()
        {
            _isPause = true;
        }

        public void Unfreeze()
        {
            _isPause = false;
        }

        #endregion Pause
    }
}