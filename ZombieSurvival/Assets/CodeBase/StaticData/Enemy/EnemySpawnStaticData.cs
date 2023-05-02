using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemySpawnStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyId EnemyId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public EnemySpawnStaticData(EnemyId id, Vector3 at)
        {
            EnemyId = id;
            Position = at;
        }

        public void OnValidate()
        {
            _inspectorName = EnemyId.ToString();
        }
    }
}