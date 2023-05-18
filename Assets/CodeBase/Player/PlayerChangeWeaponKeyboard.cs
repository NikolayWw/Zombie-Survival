using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerChangeWeaponKeyboard : MonoBehaviour
    {
        private WeaponSlotsHandler _slotsHandler;
        private IInputService _inputService;

        public void Construct(ILogicFactory logicFactory, IInputService inputService)
        {
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _inputService = inputService;

            _inputService.OnPressSelectSlot += SelectSlot;
        }

        private void OnDestroy() =>
            _inputService.OnPressSelectSlot -= SelectSlot;

        private void SelectSlot(int index) =>
            _slotsHandler.ChangeSlotSelect(index);
    }
}