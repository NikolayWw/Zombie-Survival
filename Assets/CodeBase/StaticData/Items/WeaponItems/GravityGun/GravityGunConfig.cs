using CodeBase.Data.WeaponInventory;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.GravityGun
{
    [Serializable]
    public class GravityGunConfig : BaseWeaponConfig
    {
        [field: SerializeField] public float RigidbodyAngularDrag { get; private set; } = 20f;
        [field: SerializeField] public float RigidbodyDrag { get; private set; } = 20f;
        [field: SerializeField] public float SpringConnectedAnchorOffsetZ { get; private set; } = 2f;
        [field: SerializeField] public float SpringBrakeForce { get; private set; } = 1500f;
        [field: SerializeField] public float TimeToSetBrakeForce { get; private set; } = 0.5f;
        [field: SerializeField] public float SpringSpring { get; private set; } = 10_000f;
        [field: SerializeField] public float SpringDamper { get; private set; } = 1000f;
        [field: SerializeField] public float PushForce { get; private set; } = 15f;

        protected override void Validate()
        {
            if (WeaponDataExtension.IsGravityGun(WeaponId) == false)
                ResetWeaponId();
        }
    }
}