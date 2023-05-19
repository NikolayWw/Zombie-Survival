using CodeBase.Logic.Pause;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player.Camera
{
    public class CameraLook : MonoBehaviour, IFreeze, IPause
    {
        private IInputService _inputService;

        private float _xRotation;
        private float _yRotation;
        private CameraConfig _config;

        private bool _isPause;
        private bool _isFreeze;

        public void Construct(IInputService inputService, IStaticDataService dataService)
        {
            _inputService = inputService;
            _config = dataService.PlayerConfig.CameraConfig;
        }

        private void Update()
        {
            UpdateLook();
        }

        #region Pause

        public void Pause()
        {
            _isPause = true;
            Lock();
        }

        public void Play()
        {
            _isPause = false;

            if (_isFreeze == false)
                Unlock();
        }

        public void Freeze()
        {
            _isFreeze = true;
            Lock();
        }

        public void Unfreeze()
        {
            _isFreeze = false;

            if (_isPause == false)
                Unlock();
        }

        private void Lock()
        {
            enabled = false;
        }

        private void Unlock()
        {
            enabled = true;
        }

        #endregion Pause

        private void UpdateLook()
        {
            if (_inputService == null)
                return;

            _xRotation -= _inputService.CameraAxis.y;
            _yRotation += _inputService.CameraAxis.x;

            _xRotation = Mathf.Clamp(_xRotation, _config.XClampDown, _config.XClampUp);
            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}