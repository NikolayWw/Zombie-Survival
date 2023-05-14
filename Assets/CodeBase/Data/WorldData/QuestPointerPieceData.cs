using CodeBase.StaticData.QuestPointer;
using System;
using UnityEngine;

namespace CodeBase.Data.WorldData
{
    [Serializable]
    public class QuestPointerPieceData
    {
        [field: SerializeField] public QuestPointerId Id { get; private set; }

        public QuestPointerPieceData(QuestPointerId id) =>
            Id = id;

        public void SetPosition(QuestPointerId id) =>
            Id = id;
    }
}