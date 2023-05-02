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
        Action OnTouchScreenCameraDown { get; set; }
        Action OnTouchScreenCameraUp { get; set; }

        void Clean();

        void SetFire(bool value);
    }
}