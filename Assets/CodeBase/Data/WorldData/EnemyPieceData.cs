using CodeBase.StaticData.Enemy;
using System;
using UnityEngine;

namespace CodeBase.Data.WorldData
{
    [Serializable]
    public class EnemyPieceData
    {
        [field: SerializeField] public EnemyId EnemyId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public float Health { get; private set; }

        public EnemyPieceData(EnemyId enemyId, Vector3 position, float health)
        {
            EnemyId = enemyId;
            Position = position;
            Health = health;
        }

        public void DecrementHealth(float health)
        {
            Health -= health;
            if (Health < 0)
                Health = 0;
        }

        public void SetPosition(Vector3 position) =>
            Position = position;
    }
}