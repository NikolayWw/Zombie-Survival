using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        #region Player

        public Vector2 MoveAxis => _mainControls.Player.Move.ReadValue<Vector2>();

        public Vector2 CameraAxis => Application.platform == RuntimePlatform.WindowsEditor ? _mainControls.Player.MouseLook.ReadValue<Vector2>() : _cameraAxis;
        public Action OnJump { get; set; }

        public Action OnInteract { get; set; }
        public Action OnUseAidKit { get; set; }

        #endregion Player

        #region Weapon

        public bool IsFirePressed => _mainControls.Weapon.Fire.IsPressed();
        public Action OnWeaponReload { get; set; }

        #endregion Weapon

        #region Inventory

        public Action<int> OnPressSelectSlot { get; set; }

        #endregion Inventory

        #region UI

        public Action OnOpenGameMenu { get; set; }

        #endregion UI

        private readonly MainControls _mainControls;
        private Vector2 _cameraAxis;

        public InputService()
        {
            _mainControls = new MainControls();
            EnableActions();

            SubscribePlayer();
            SubscribeWeapon();
            SubscribeInventory();
            SubscribeUI();
        }

        public void Clean()
        {
            OnJump = null;
            OnInteract = null;
            OnUseAidKit = null;
            OnWeaponReload = null;
            OnOpenGameMenu = null;
            OnPressSelectSlot = null;
        }

        public void UpdateCameraAxis(Vector2 axis) =>
            _cameraAxis = axis;

        private void EnableActions()
        {
            _mainControls.Player.Enable();
            _mainControls.Weapon.Enable();
            _mainControls.Inventory.Enable();
            _mainControls.UI.Enable();
        }

        private void SubscribePlayer()
        {
            _mainControls.Player.Jump.performed += _ => OnJump?.Invoke();
            _mainControls.Player.Interact.performed += _ => OnInteract?.Invoke();
            _mainControls.Player.UseAidKit.performed += _ => OnUseAidKit?.Invoke();
        }

        private void SubscribeWeapon()
        {
            _mainControls.Weapon.Reload.performed += _ => OnWeaponReload?.Invoke();
        }

        private void SubscribeInventory()
        {
            _mainControls.Inventory.SelectWeaponSlot.performed += (index) => OnPressSelectSlot?.Invoke((int)index.ReadValue<float>());
        }

        private void SubscribeUI()
        {
            _mainControls.UI.OpenGameMenu.performed += _ => OnOpenGameMenu?.Invoke();
        }
    }
}