using CodeBase.Logic.ApplyDamage;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Props
{
    [Serializable]
    public class PropsConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public PropsId PropsId { get; private set; }
        [field: SerializeField] public ApplyDamageSurfaceId ApplyDamageSurfaceId { get; private set; }
        [field: SerializeField] public GameObject PrefabInPiece { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField] public GameObject[] PropsFxPrefabs { get; private set; }

        public void OnValidate()
        {
            _inspectorName = PropsId.ToString();

            if (MaxHealth < 0) MaxHealth = 0;
        }
    }
}