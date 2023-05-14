using System;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [Serializable]
    public class NpcSpawnStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public NpcId NpcId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }

        public NpcSpawnStaticData(NpcId id, Vector3 position, Quaternion rotation)
        {
            NpcId = id;
            Position = position;
            Rotation = rotation;
        }

        public void OnValidate()
        {
            _inspectorName = NpcId.ToString();
        }
    }
}