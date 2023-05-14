using CodeBase.StaticData.Items.WeaponItems;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Shop
{
    [Serializable]
    public class ShopConfig
    {
        [SerializeField] private string _inspectorName = string.Empty;
        [field: SerializeField] public bool IsAidKit { get; private set; }
        [field: SerializeField] public WeaponId WeaponId { get; private set; }

        public void OnValidate()
        {
            _inspectorName = IsAidKit ? "AidKit" : WeaponId.ToString();
        }
    }
}