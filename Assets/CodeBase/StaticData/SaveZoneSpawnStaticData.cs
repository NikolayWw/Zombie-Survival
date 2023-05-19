using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class SaveZoneSpawnStaticData
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotate { get; private set; }

        public SaveZoneSpawnStaticData(Vector3 position, Quaternion rotate)
        {
            Position = position;
            Rotate = rotate;
        }
    }
}