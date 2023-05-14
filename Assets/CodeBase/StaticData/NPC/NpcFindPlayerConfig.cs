using System;
using UnityEngine;

namespace CodeBase.StaticData.NPC
{
    [Serializable]
    public class NpcFindPlayerConfig
    {
        [field: SerializeField] public LayerMask PlayerLayer { get; private set; } = 1 << 8;
        [field: SerializeField] public float FindDelay { get; private set; } = 1f;
    }
}