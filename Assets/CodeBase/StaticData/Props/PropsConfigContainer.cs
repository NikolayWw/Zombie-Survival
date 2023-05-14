using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Props
{
    [CreateAssetMenu(fileName = "New PropsConfigContainer", menuName = "Static Data/Props Config Container", order = 0)]
    public class PropsConfigContainer : ScriptableObject
    {
        [field: SerializeField] public List<PropsConfig> Configs { get; private set; }
        [field: SerializeField] public float LifeTimeFx { get; private set; } = 5;
        [field: SerializeField] public float FxSpreadForce { get; private set; } = 5;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}