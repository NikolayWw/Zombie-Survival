using System;
using UnityEngine;

namespace CodeBase.StaticData.Player.Move
{
    [Serializable]
    public class StairMoveConfig
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}