using CodeBase.Data.QuestItemInventory;
using CodeBase.Logic.Inventory.QuestItemInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.QuestInventory
{
    public class UIQuestItemsSlotsContainer : MonoBehaviour
    {
        private readonly List<UIQuestItemSlotUpdate> _uiSlots = new List<UIQuestItemSlotUpdate>();

        private QuestSlotsContainer _slotsContainer;
        private QuestSlotsHandler _slotsHandler;
        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory, IPersistentProgressService persistentProgressService, ILogicFactory logicFactory)
        {
            _uiFactory = uiFactory;
            _slotsContainer = persistentProgressService.PlayerProgress.QuestSlotsContainer;
            _slotsHandler = logicFactory.QuestSlotsHandler;

            _slotsHandler.OnSlotsDataChange += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            _slotsHandler.OnSlotsDataChange -= Refresh;
        }

        private void Refresh()
        {
            ResizeUISlots();

            for (int i = 0; i < _slotsContainer.Slots.Count; i++)
            {
                QuestSlot dataSlot = _slotsContainer.Slots[i];
                _uiSlots[i].Refresh(dataSlot.Id, dataSlot.Amount);
            }
        }

        private void ResizeUISlots()
        {
            _uiSlots.ForEach(x => x.Remove());
            _uiSlots.Clear();

            foreach (var slot in _slotsContainer.Slots)
            {
                var uiSlot = _uiFactory.CreateQuestItemInventorySlot(transform);
                uiSlot.Refresh(slot.Id, slot.Amount);
                _uiSlots.Add(uiSlot);
            }
        }
    }
}