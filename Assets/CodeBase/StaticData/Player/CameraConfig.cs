using System;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [Serializable]
    public class CameraConfig
    {
        [field: SerializeField] public float XClampUp { get; private set; } = 90f;
        [field: SerializeField] public float XClampDown { get; private set; } = -90f;
    }
}