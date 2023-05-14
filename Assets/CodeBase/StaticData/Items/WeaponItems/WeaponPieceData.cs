using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    [Serializable]
    public class WeaponPieceData
    {
        [field: SerializeField] public WeaponDataContainer WeaponDataContainer { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public WeaponPieceData(WeaponDataContainer weaponDataContainer, Vector3 position)
        {
            WeaponDataContainer = weaponDataContainer;
            Position = position;
        }

        public void SetPosition(Vector3 position) =>
            Position = position;
    }
}