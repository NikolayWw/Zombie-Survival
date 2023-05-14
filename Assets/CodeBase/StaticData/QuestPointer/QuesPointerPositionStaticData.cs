using System;
using UnityEngine;

namespace CodeBase.StaticData.QuestPointer
{
    [Serializable]
    public class QuesPointerPositionStaticData
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public QuestPointerId Id { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public QuesPointerPositionStaticData(QuestPointerId id, Vector3 position)
        {
            Id = id;
            Position = position;
        }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}