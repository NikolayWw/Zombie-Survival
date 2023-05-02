using System;
using UnityEngine;

namespace CodeBase.StaticData.Player.Move
{
    [Serializable]
    public class SwitchMoveConfig
    {
        [field: SerializeField] public float DelayCheck { get; private set; } = 0.1f;
        [field: SerializeField] public float SphereOffsetY { get; private set; }
        [field: SerializeField] public float SphereRadius { get; private set; }
        [field: SerializeField] public LayerMask CheckLayers { get; private set; }

        public void OnValidate()
        {
            if (DelayCheck < 0.1f) DelayCheck = 0.1f;
        }
    }
}