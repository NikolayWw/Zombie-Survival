using CodeBase.Data.QuestItemInventory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.QuestItems;
using System;

namespace CodeBase.Logic.Inventory.QuestItemInventory
{
    public class QuestSlotsHandler
    {
        private readonly IStaticDataService _dataService;
        private readonly QuestSlotsContainer _inventoryContainer;
        public Action OnSlotsDataChange;

        public QuestSlotsHandler(IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _dataService = dataService;
            _inventoryContainer = persistentProgressService.PlayerProgress.QuestSlotsContainer;
        }

        public void Add(QuestItemId id, int amount)
        {
            QuestItemConfig config = _dataService.ForQuestItem(id);
            if (config.CanStack == false)
            {
                _inventoryContainer.AddSlot(id, amount);
                OnSlotsDataChange?.Invoke();
            }
            else
            {
                foreach (var slot in _inventoryContainer.Slots)
                {
                    if (slot.Id == id)
                    {
                        slot.AddAmount(amount);
                        OnSlotsDataChange?.Invoke();
                        return;
                    }
                }
                _inventoryContainer.AddSlot(id, amount);
                OnSlotsDataChange?.Invoke();
            }
        }

        public void DecrementItem(QuestItemId id, int amount)
        {
            foreach (var slot in _inventoryContainer.Slots)
            {
                if (slot.Id == id)
                {
                    if (slot.Amount - amount > 0)
                    {
                        slot.DecrementAmount(amount);
                    }
                    else
                    {
                        slot.Clean();
                        _inventoryContainer.Slots.Remove(slot);
                    }
                    break;
                }
            }
            OnSlotsDataChange?.Invoke();
        }

        public bool ContainsItem(QuestItemId id, int amount)
        {
            int amountCount = 0;
            foreach (var slot in _inventoryContainer.Slots)
            {
                if (slot.Id == id)
                    amountCount += slot.Amount;

                if (amountCount >= amount)
                    return true;
            }
            return false;
        }
    }
}