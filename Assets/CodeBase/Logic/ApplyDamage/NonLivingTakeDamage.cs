using System;
using UnityEngine;

namespace CodeBase.Logic.ApplyDamage
{
    public class NonLivingTakeDamage : MonoBehaviour, IApplyDamage
    {
        [field: SerializeField] public ApplyDamageSurfaceId SurfaceId { get; private set; }
        public Action OnDestroy { get; set; }

        public void ApplyDamage(float damage, Vector3 directionAndForce)
        { }
    }
}