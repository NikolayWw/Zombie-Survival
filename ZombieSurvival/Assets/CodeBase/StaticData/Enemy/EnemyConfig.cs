using CodeBase.Logic.ApplyDamage;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public EnemyId EnemyId { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float WalkingSpeed { get; private set; } = 1f;
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField] public float Damage { get; private set; } = 1f;
        [field: SerializeField] public float AttackDelay { get; private set; } = 1f;
        [field: SerializeField] public float AttackDistance { get; private set; } = 1f;

        [field: SerializeField] public LayerMask WhatIsTargetLayer { get; private set; }
        [field: SerializeField] public float FindTargetRadius { get; private set; }
        [field: SerializeField] public float MoveToTargetSpeed { get; private set; }
        [field: SerializeField] public ApplyDamageSurfaceId ApplyDamageSurfaceId { get; private set; }
    }
}