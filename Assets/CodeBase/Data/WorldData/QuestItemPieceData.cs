using CodeBase.StaticData.Items.QuestItems;
using System;
using UnityEngine;

namespace CodeBase.Data.WorldData
{
    [Serializable]
    public class QuestItemPieceData
    {
        [field: SerializeField] public QuestItemId ItemId { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public QuestItemPieceData(QuestItemId itemId, int amount, Vector3 position)
        {
            ItemId = itemId;
            Amount = amount;
            Position = position;
        }
    }
}