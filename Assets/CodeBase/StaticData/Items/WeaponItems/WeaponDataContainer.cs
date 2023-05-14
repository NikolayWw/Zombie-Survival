using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.GravityGun;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    [Serializable]
    public class WeaponDataContainer
    {
        [HideInInspector] [SerializeField] private FirearmWeaponData _firearmFirearmWeaponData;
        [HideInInspector] [SerializeField] private MeleeWeaponData _meleeWeaponData;
        [HideInInspector] [SerializeField] private GravityGunData _gravityGunData;

        public WeaponDataContainer(MeleeWeaponData meleeWeaponData) =>
            _meleeWeaponData = meleeWeaponData;

        public WeaponDataContainer(FirearmWeaponData firearmWeapon) =>
            _firearmFirearmWeaponData = firearmWeapon;

        public WeaponDataContainer(GravityGunData gravityGunData) =>
            _gravityGunData = gravityGunData;

        public BaseWeaponData GetData()
        {
            if (_meleeWeaponData != null && _meleeWeaponData.WeaponId != WeaponId.None)
                return _meleeWeaponData;

            if (_firearmFirearmWeaponData != null && _firearmFirearmWeaponData.WeaponId != WeaponId.None)
                return _firearmFirearmWeaponData;

            if (_gravityGunData != null && _gravityGunData.WeaponId != WeaponId.None)
                return _gravityGunData;

            return null;
        }
    }
}