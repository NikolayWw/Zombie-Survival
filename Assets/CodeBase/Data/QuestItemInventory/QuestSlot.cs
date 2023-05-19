using CodeBase.StaticData.Items.QuestItems;
using System;
using UnityEngine;

namespace CodeBase.Data.QuestItemInventory
{
    [Serializable]
    public class QuestSlot
    {
        [field: SerializeField] public QuestItemId Id { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        public Action OnChangeValue;

        public QuestSlot(QuestItemId id, int amount)
        {
            SetItem(id, amount);
        }

        public bool IsEmpty() =>
            Id == QuestItemId.None;

        public void AddAmount(int amount)
        {
            Amount += amount;
            OnChangeValue?.Invoke();
        }

        public void DecrementAmount(int amount)
        {
            Amount -= amount;
            OnChangeValue?.Invoke();
        }

        public void Clean()
        {
            Id = QuestItemId.None;
            Amount = 0;
            OnChangeValue?.Invoke();
        }

        private void SetItem(QuestItemId id, int amount)
        {
            Id = id;
            Amount = amount;
            OnChangeValue?.Invoke();
        }
    }
}