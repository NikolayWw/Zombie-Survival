using System;
using UnityEngine;

namespace CodeBase.StaticData.Player.Move
{
    [Serializable]
    public class MoveStaticData
    {
        [field: SerializeField] public GroundMoveConfig GroundMoveConfig { get; private set; }
        [field: SerializeField] public StairMoveConfig StairMoveConfig { get; private set; }
        [field: SerializeField] public SwitchMoveConfig SwitchMoveConfig { get; private set; }
        [field: SerializeField] public float BodyRotateLerpRate { get; private set; } = 1f;
    }
}