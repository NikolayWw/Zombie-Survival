using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems.ShotEffect
{
    [CreateAssetMenu(fileName = "New ShotEffectContainer", menuName = "Static Data/Shot Effect Configs Container", order = 0)]
    public class ShotEffectConfigsContainer : ScriptableObject
    {
        [field: SerializeField] public List<ShotEffectConfig> Configs { get; private set; }

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}