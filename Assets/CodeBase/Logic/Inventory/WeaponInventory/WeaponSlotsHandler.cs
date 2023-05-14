using CodeBase.Data.WeaponInventory;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.UI.Services.Factory;
using System;
using System.Linq;

namespace CodeBase.Logic.Inventory.WeaponInventory
{
    public class WeaponSlotsHandler
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly WeaponSlotsContainer _slots;
        public Action<UpdateWeaponSlotType> OnSelectedSlotHaveBeenUpdated;

        public WeaponSlotsHandler(IPersistentProgressService persistentProgressService, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _slots = persistentProgressService.PlayerProgress.WeaponSlotsContainer;
        }

        public void Add(WeaponDataContainer weaponDataContainer)
        {
            foreach (WeaponSlot slot in _slots.Slots)
            {
                if (slot.IsEmpty() == false)
                {
                    var currentData = slot.WeaponDataContainer.GetData();
                    if (currentData.WeaponId == weaponDataContainer.GetData().WeaponId && CanStack(currentData.WeaponId))
                    {
                        ChangeWeaponData(weaponDataContainer, currentData);
                        break;
                    }
                }
                else if (slot.IsEmpty())
                {
                    slot.SetWeapon(weaponDataContainer);

                    if (slot.IsSelected())
                        OnSelectedSlotHaveBeenUpdated?.Invoke(UpdateWeaponSlotType.AddItem);

                    break;
                }
            }
        }

        public void Drop(int slotIndex)
        {
            WeaponSlot slot = _slots.Slots[slotIndex];
            _gameFactory.CreateWeaponPiece(new WeaponPieceData(slot.WeaponDataContainer, _gameFactory.Player.transform.position), _uiFactory);
            slot.Clear();

            if (_slots.Slots[slotIndex].IsSelected())
                OnSelectedSlotHaveBeenUpdated?.Invoke(UpdateWeaponSlotType.Drop);
        }

        public void ChangeSlotSelect(int index)
        {
            foreach (var slot in _slots.Slots.Where(slot => slot.IsSelected()))
            {
                if (slot.SlotIndex == index)
                {
                    slot.DeSelected();
                    OnSelectedSlotHaveBeenUpdated?.Invoke(UpdateWeaponSlotType.SelectSlot);
                    return;
                }

                slot.DeSelected();
            }
            _slots.Slots[index].SetSelected();
            OnSelectedSlotHaveBeenUpdated?.Invoke(UpdateWeaponSlotType.SelectSlot);
        }

        public bool CanAdd(WeaponId id)
        {
            if (IsInventoryFull())
            {
                return IsWeaponPresentInInventory(id) && CanStack(id);
            }
            return true;
        }

        public bool CanStack(WeaponId id)
        {
            return WeaponDataExtension.IsFirearmsWeapon(id);
        }

        public bool IsCurrentSelectAndWeaponPresent(out WeaponSlot weaponSlot) =>
            IsCurrentSelect(out weaponSlot) && weaponSlot.IsEmpty() == false;

        public bool IsWeaponPresentInInventory(WeaponId id)
        {
            foreach (WeaponSlot slot in _slots.Slots)
            {
                if (slot.IsEmpty() == false && slot.WeaponDataContainer.GetData().WeaponId == id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsWeaponPresentInInventory(WeaponId id, out WeaponSlot weaponSlot)
        {
            foreach (WeaponSlot slot in _slots.Slots)
            {
                if (slot.IsEmpty() == false && slot.WeaponDataContainer.GetData().WeaponId == id)
                {
                    weaponSlot = slot;
                    return true;
                }
            }

            weaponSlot = null;
            return false;
        }

        public bool IsCurrentSelect(out WeaponSlot weaponSlot)
        {
            foreach (var slot in _slots.Slots)
            {
                if (slot.IsSelected())
                {
                    weaponSlot = slot;
                    return true;
                }
            }

            weaponSlot = null;
            return false;
        }

        public bool IsInventoryFull() =>
            _slots.Slots.All(slot => slot.IsEmpty() == false);

        private static void ChangeWeaponData(WeaponDataContainer newWeaponDataContainer, BaseWeaponData currentData)
        {
            switch (currentData)
            {
                case FirearmWeaponData data:
                    if (newWeaponDataContainer.GetData() is FirearmWeaponData firearmWeaponData)
                    {
                        data.SetAmmoValue(data.Magazine, data.Amount + firearmWeaponData.Magazine + firearmWeaponData.Amount);
                    }
                    break;
            }
        }
    }
}