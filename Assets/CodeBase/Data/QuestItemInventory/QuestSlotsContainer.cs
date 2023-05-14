using CodeBase.StaticData.Items.QuestItems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.QuestItemInventory
{
    [Serializable]
    public class QuestSlotsContainer
    {
        [field: SerializeField] public List<QuestSlot> Slots { get; private set; } = new List<QuestSlot>();

        public void AddSlot(QuestItemId id, int amount)
        {
            Slots.Add(new QuestSlot(id, amount));
        }
    }
}