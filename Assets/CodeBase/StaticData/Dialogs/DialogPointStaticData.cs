using CodeBase.StaticData.NPC;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Dialogs
{
    [Serializable]
    public class DialogPointStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public NpcId QuestPointId { get; private set; }
        [field: SerializeField] public float SphereRadius { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public DialogPointStaticData(NpcId questPointId, float sphereRadius, Vector3 position)
        {
            QuestPointId = questPointId;
            SphereRadius = sphereRadius;
            Position = position;
        }

        public void OnValidate()
        {
            _inspectorName = QuestPointId.ToString();
        }
    }
}