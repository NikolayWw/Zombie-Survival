using CodeBase.Data.WeaponInventory;
using CodeBase.Weapon.Firearms;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon
{
    [Serializable]
    public class FirearmsConfig : BaseWeaponConfig
    {
        [field: SerializeField] public CrosshairConfig CrosshairConfig { get; private set; }
        [field: SerializeField] public FirearmWeaponShootType ShootType { get; private set; }
        [field: SerializeField] public int MaxMagazine { get; private set; } = 1;
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public int BuyAmount { get; private set; } = 1;
        [field: SerializeField] public AudioClip FireAudio { get; private set; }
        [field: SerializeField] public AudioClip EmptyFireAudio { get; private set; }
        [field: SerializeField] public AudioClip ReloadAudio { get; private set; }

        protected override void Validate()
        {
            if (MaxMagazine < 1) MaxMagazine = 1;
            if (BuyAmount < 1) BuyAmount = 1;

            if (WeaponDataExtension.IsFirearmsWeapon(WeaponId) == false)
                ResetWeaponId();
        }
    }
}