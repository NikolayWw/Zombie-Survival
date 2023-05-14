using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public Vector2 MoveAxis => _mainControls.Player.Move.ReadValue<Vector2>();

        public Vector2 CameraAxis => Application.isEditor ? _mainControls.Player.MouseLook.ReadValue<Vector2>() : _cameraAxis;
        public Action OnJump { get; set; }

        public Action OnInteract { get; set; }
        public Action OnUseAidKit { get; set; }

        public bool IsFirePressed => _mainControls.Weapon.Fire.IsPressed();
        public Action OnWeaponReload { get; set; }

        private readonly MainControls _mainControls;
        private Vector2 _cameraAxis;

        public InputService()
        {
            _mainControls = new MainControls();
            _mainControls.Player.Enable();
            _mainControls.Weapon.Enable();

            _mainControls.Player.Jump.performed += _ => OnJump?.Invoke();
            _mainControls.Player.Interact.performed += _ => OnInteract?.Invoke();
            _mainControls.Player.UseAidKit.performed += _ => OnUseAidKit?.Invoke();

            _mainControls.Weapon.Reload.performed += _ => OnWeaponReload?.Invoke();
        }

        public void Clean()
        {
            OnJump = null;
            OnInteract = null;
            OnUseAidKit = null;
            OnWeaponReload = null;
        }

        public void UpdateCameraAxis(Vector2 axis) =>
            _cameraAxis = axis;
    }
}