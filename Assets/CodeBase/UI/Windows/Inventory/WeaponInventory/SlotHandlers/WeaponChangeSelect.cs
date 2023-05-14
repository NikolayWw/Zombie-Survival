using CodeBase.Infrastructure.Logic;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Logic.Pause;
using CodeBase.Services.LogicFactory;
using CodeBase.UI.Windows.Inventory.WeaponInventory.Logic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.SlotHandlers
{
    public class WeaponChangeSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ICoroutineRunner, IFreeze
    {
        private WeaponSlotsHandler _slotsHandler;
        private int _index;
        private bool _isPause;
        private UIWeaponTimer _tapUIWeaponTimer;

        public void Construct(ILogicFactory logicFactory, int index)
        {
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _index = index;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPause)
                return;

            _tapUIWeaponTimer = new UIWeaponTimer(this, 0.1f);  //tap delay
            _tapUIWeaponTimer.Start();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isPause)
                return;

            if (_tapUIWeaponTimer.Elapsed == false)
                _slotsHandler.ChangeSlotSelect(_index);
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