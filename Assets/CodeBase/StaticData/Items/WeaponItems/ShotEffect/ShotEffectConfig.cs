using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.ShotEffect
{
    [Serializable]
    public class ShotEffectConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ShotEffectId ShotEffectId { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = ShotEffectId.ToString();
        }
    }
}