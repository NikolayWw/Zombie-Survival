using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player.Camera
{
    public class CameraLook : MonoBehaviour
    {
        private IInputService _inputService;

        private float _xRotation;
        private float _yRotation;
        private CameraConfig _config;

        public void Construct(IInputService inputService, IStaticDataService dataService)
        {
            _inputService = inputService;
            _config = dataService.PlayerConfig.CameraConfig;
        }

        private void Update()
        {
            UpdateLook();
        }

        private void UpdateLook()
        {
            if (_inputService == null)
                return;

            _xRotation -= _inputService.CameraAxis.y * _config.Slowdown;
            _yRotation += _inputService.CameraAxis.x * _config.Slowdown;

            _xRotation = Mathf.Clamp(_xRotation, _config.XClampDown, _config.XClampUp);
            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}