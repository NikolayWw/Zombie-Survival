using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Minimap;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Windows.Minimap
{
    public class Minimap : MonoBehaviour
    {
        [Serializable]
        private class ClampPositionMinimapCameraSettings
        {
            [field: SerializeField] public float XMinZoomPosLeft { get; private set; }
            [field: SerializeField] public float XMinZoomPosRight { get; private set; }
            [field: SerializeField] public float XMaxZoomPosLeft { get; private set; }
            [field: SerializeField] public float XMaxZoomPosRight { get; private set; }
            [field: SerializeField] public float ZMinZoomPosUp { get; private set; }
            [field: SerializeField] public float ZMinZoomPosDown { get; private set; }
            [field: SerializeField] public float ZMaxZoomPosUp { get; private set; }
            [field: SerializeField] public float ZMaxZoomPosDown { get; private set; }
        }

        [SerializeField] private Camera _mapCamera;
        [SerializeField] private ClampPositionMinimapCameraSettings _clampPosSettings;

        private List<WorldMinimapIcon> _icons;
        private MinimapConfig _config;
        private Transform _followTarget;

        public void Construct(IGameFactory gameFactory, IStaticDataService dataService, Transform followTarget)
        {
            _icons = gameFactory.MinimapIcons;
            _config = dataService.MinimapConfig;
            _followTarget = followTarget;
            _mapCamera.targetTexture = _config.RenderTexture;
        }

        private void Start()
        {
            RefreshZoom();
            RefreshSizeIcons();
        }

        private void LateUpdate()
        {
            UpdatePosition();
        }

        private void RefreshSizeIcons()
        {
            for (var i = 0; i < _icons.Count; i++)
                _icons[i].Resize(_config.StartZoom);
        }

        private void UpdatePosition()
        {
            var targetPosition = _followTarget.position;
            targetPosition.x = ClampPosition(targetPosition.x, _clampPosSettings.XMinZoomPosLeft, _clampPosSettings.XMaxZoomPosLeft, _clampPosSettings.XMinZoomPosRight, _clampPosSettings.XMaxZoomPosRight);
            targetPosition.z = ClampPosition(targetPosition.z, _clampPosSettings.ZMinZoomPosDown, _clampPosSettings.ZMaxZoomPosDown, _clampPosSettings.ZMinZoomPosUp, _clampPosSettings.ZMaxZoomPosUp);
            _mapCamera.transform.position = new Vector3(targetPosition.x, _mapCamera.transform.position.y, targetPosition.z);
        }

        private float ClampPosition(float position, float minLeft, float maxLeft, float minRight, float maxRight)
        {
            float leftSide = Mathf.Lerp(minLeft, maxLeft, _config.StartZoom);
            float rightSide = Mathf.Lerp(minRight, maxRight, _config.StartZoom);
            return Mathf.Clamp(position, leftSide, rightSide);
        }

        private void RefreshZoom() =>
            _mapCamera.orthographicSize = Mathf.Lerp(_config.MinSizeZoom, _config.MaxSizeZoom, _config.StartZoom);
    }
}