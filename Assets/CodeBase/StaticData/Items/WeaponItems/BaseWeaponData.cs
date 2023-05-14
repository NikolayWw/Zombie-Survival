using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    public abstract class BaseWeaponData
    {
        public Action OnChangeValue;

        [field: SerializeField] public WeaponId WeaponId { get; private set; }

        protected BaseWeaponData(WeaponId weaponId)
        {
            WeaponId = weaponId;
        }
    }
}