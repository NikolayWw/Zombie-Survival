using UnityEngine;

namespace CodeBase.StaticData.Minimap
{
    [CreateAssetMenu(fileName = "New MinimapConfig", menuName = "Static Data/Minimap Config", order = 0)]
    public class MinimapConfig : ScriptableObject
    {
        [field: SerializeField] public RenderTexture RenderTexture { get; private set; }
        [field: SerializeField] public float MinSizeZoom { get; private set; } = 1f;
        [field: SerializeField] public float MaxSizeZoom { get; private set; } = 75f;

        [Range(0f, 1f)] [SerializeField] private float _startZoom;
        public float StartZoom => _startZoom;
    }
}