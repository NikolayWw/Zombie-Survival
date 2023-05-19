using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.ShotEffect
{
    [CreateAssetMenu(fileName = "New ShootEffectSettingsContainer", menuName = "Static Data/Shot Effect Settings Container", order = 0)]
    public class ShootEffectSettingsContainer : ScriptableObject
    {
        [field: SerializeField] public List<ShotEffectSettings> Settings { get; private set; }

        private void OnValidate()
        {
            Settings.ForEach(x => x.OnValidate());
        }
    }
}