using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;
using UnityEngine;

namespace CodeBase.Npc.Dialogs
{
    public struct MovePointerConditionContainer
    {
        [field: SerializeField] public QuestItemId QuestItemId { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public QuestPointerId PointerId { get; private set; }

        public MovePointerConditionContainer(QuestItemId questItemId, int amount, QuestPointerId pointerId)
        {
            QuestItemId = questItemId;
            Amount = amount;
            PointerId = pointerId;
        }
    }
}