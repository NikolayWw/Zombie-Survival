using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    [Serializable]
    public class WeaponStaticData
    {
        [SerializeField] private string _inscpectorName;
        [field: SerializeField] public WeaponDataContainer WeaponDataContainer { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public WeaponStaticData(WeaponDataContainer weaponDataContainer, Vector3 position)
        {
            WeaponDataContainer = weaponDataContainer;
            Position = position;
        }

        public void OnValidate()
        {
            _inscpectorName = WeaponDataContainer?.GetData()?.WeaponId.ToString();
        }
    }
}