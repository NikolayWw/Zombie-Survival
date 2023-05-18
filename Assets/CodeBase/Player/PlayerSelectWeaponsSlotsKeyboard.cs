using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Logic.Pause;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSelectWeaponsSlotsKeyboard : MonoBehaviour, IFreeze
    {
        private WeaponSlotsHandler _slotsHandler;
        private IInputService _inputService;

        public void Construct(ILogicFactory logicFactory, IInputService inputService)
        {
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _inputService = inputService;

            Subscribe(true);
        }

        public void Freeze()
        {
            Subscribe(false);
        }

        public void Unfreeze()
        {
            Subscribe(true);
        }

        private void OnDestroy() =>
            Subscribe(false);

        private void SelectSlot(int index) =>
            _slotsHandler.ChangeSlotSelect(index);

        private void Subscribe(bool isSubscribe)
        {
            if (isSubscribe)
            {
                _inputService.OnPressSelectSlot += SelectSlot;
            }
            else
            {
                _inputService.OnPressSelectSlot -= SelectSlot;
            }
        }
    }
}