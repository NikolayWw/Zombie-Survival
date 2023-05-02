using System;
using UnityEngine;

namespace CodeBase.StaticData.Minimap
{
    [Serializable]
    public class MinimapWorldIconConfig
    {
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public Vector3 MinZoomScale { get; set; } = Vector3.one;
        [field: SerializeField] public Vector3 MaxZoomScale { get; set; } = Vector3.one;
    }
}