using CodeBase.Data.WeaponInventory;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.MeleeWeapon
{
    [Serializable]
    public class MeleeWeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public AudioClip AttackAudio { get; private set; }

        protected override void Validate()
        {
            if (WeaponDataExtension.IsMeleeWeapon(WeaponId) == false)
                ResetWeaponId();
        }
    }
}