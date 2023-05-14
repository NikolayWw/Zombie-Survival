using CodeBase.Services.Factory;
using CodeBase.StaticData.Minimap;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Windows.Minimap
{
    public class WorldMinimapIcon : MonoBehaviour
    {
        [SerializeField] private Transform _mapIconTransform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private MinimapWorldIconConfig _config;
        private List<WorldMinimapIcon> _icons;

        public void Construct(MinimapWorldIconConfig config, IGameFactory gameFactory)
        {
            _config = config;
            _icons = gameFactory.MinimapIcons;
            _spriteRenderer.sprite = config.Icon;
        }

        private void OnDestroy() =>
            _icons.Remove(this);

        public void Resize(float size) =>
            _mapIconTransform.localScale = Vector3.Lerp(_config.MinZoomScale, _config.MaxZoomScale, size);
    }
}