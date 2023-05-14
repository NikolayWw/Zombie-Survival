using System;
using UnityEngine;

namespace CodeBase.StaticData.Props
{
    [Serializable]
    public class PropsSpawnStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public PropsId PropsId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public PropsSpawnStaticData(PropsId id, Vector3 at)
        {
            PropsId = id;
            Position = at;
        }

        public void OnValidate()
        {
            _inspectorName = PropsId.ToString();
        }
    }
}