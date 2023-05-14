using System;
using UnityEngine;

namespace CodeBase.Logic.ApplyDamage
{
    public interface IApplyDamage
    {
        ApplyDamageSurfaceId SurfaceId { get; }

        void ApplyDamage(float damage, Vector3 directionAndForce);

        Action OnDestroy { get; set; }
    }
}