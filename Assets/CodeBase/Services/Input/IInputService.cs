using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Action OnJump { get; set; }
        Action OnInteract { get; set; }
        Action OnWeaponReload { get; set; }
        bool IsFirePressed { get; }
        Action OnUseAidKit { get; set; }
        Vector2 CameraAxis { get; }
        Action<int> OnPressSelectSlot { get; set; }
        Action OnOpenGameMenu { get; set; }

        void Clean();

        void UpdateCameraAxis(Vector2 axis);
    }
}