using CodeBase.StaticData.NPC;
using System;
using UnityEngine;

namespace CodeBase.Data.WorldData
{
    [Serializable]
    public class NpcPieceData
    {
        [field: SerializeField] public NpcId NpcId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotate { get; private set; }

        public NpcPieceData(NpcId id, Vector3 position, Quaternion rotate)
        {
            NpcId = id;
            Position = position;
            Rotate = rotate;
        }
    }
}