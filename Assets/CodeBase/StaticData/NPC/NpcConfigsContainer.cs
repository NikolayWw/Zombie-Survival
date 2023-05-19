using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [CreateAssetMenu(fileName = "New NpcConfigsContainer", menuName = "Static Data/Npc Configs Container", order = 0)]
    public class NpcConfigsContainer : ScriptableObject
    {
        [field: SerializeField] public NpcFindPlayerConfig NpcFindPlayerConfig { get; private set; }
        [field: SerializeField] public List<NpcConfig> NpcConfigs { get; private set; }

        private void OnValidate()
        {
            NpcConfigs.ForEach(x => x.OnValidate());
        }
    }
}