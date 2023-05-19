using CodeBase.Logic.ApplyDamage;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.ShotEffect
{
    [Serializable]
    public class ShotEffectSettings
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ApplyDamageSurfaceId ApplyDamageSurfaceId { get; private set; }
        [field: SerializeField] public ShotEffectId ShotEffectId { get; private set; }
        [field: SerializeField] public ShotEffectId HoleEffectId { get; private set; }

        public void OnValidate()
        {
            _inspectorName = ApplyDamageSurfaceId.ToString();
        }
    }
}