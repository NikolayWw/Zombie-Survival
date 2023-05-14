using System;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [Serializable]
    public class CameraConfig
    {
        [field: SerializeField] public float XClampUp { get; private set; } = 90f;
        [field: SerializeField] public float XClampDown { get; private set; } = -90f;
        [field: SerializeField] public float Slowdown { get; private set; } = 0.1f;

        public void OnValidate()
        {
            Slowdown = Mathf.Clamp01(Slowdown);
        }
    }
}