using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.QuestItems
{
    [Serializable]
    public class QuestItemStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public QuestItemId Id { get; private set; }
        [field: SerializeField] public int Amount { get; private set; } = 1;
        [field: SerializeField] public Vector3 Position { get; private set; }

        public QuestItemStaticData(QuestItemId id, int amount, Vector3 position)
        {
            Id = id;
            Amount = amount;
            Position = position;
        }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
            if (Amount < 1) Amount = 1;
        }
    }
}