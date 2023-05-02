using System;
using UnityEngine;

namespace CodeBase.StaticData.Player.Move
{
    [Serializable]
    public class GroundMoveConfig
    {
        [field: SerializeField] public float Speed { get; private set; } = 1f;
        [field: SerializeField] public float JumpForce { get; private set; } = 1f;

        [field: SerializeField] public LayerMask WhatIsGround { get; private set; }
        [field: SerializeField] public float ConstantForceY { get; private set; }
        [field: SerializeField] public float GroundSphereOffsetY { get; private set; } = -0.5f;
        [field: SerializeField] public float GroundSphereRadius { get; private set; } = 0.5f;
    }
}