using CodeBase.StaticData.Minimap;
using CodeBase.StaticData.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [Serializable]
    public class NpcConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public NpcId NpcId { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public MinimapWorldIconConfig IconConfig { get; private set; }
        [field: SerializeField] public List<ShopConfig> ShopConfigs { get; private set; }

        public void OnValidate()
        {
            _inspectorName = NpcId.ToString();
            ShopConfigs.ForEach(x => x.OnValidate());
        }
    }
}