using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon
{
    [Serializable]
    public class CrosshairConfig
    {
        [field: SerializeField] public float Width { get; private set; } = 10f;
        [field: SerializeField] public float Lenght { get; private set; } = 56f;
        [field: SerializeField] public float MinSize { get; private set; } = 40f;
        [field: SerializeField] public Color Color { get; private set; } = Color.red;
    }
}