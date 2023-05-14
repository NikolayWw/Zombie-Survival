using CodeBase.Services;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.UI.Windows;
using System;

namespace CodeBase.UI.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowId id);

        void Close(WindowId id);

        bool GetWindow<TWindow>(WindowId id, out TWindow window) where TWindow : BaseWindow;

        void OpenUpdateWeaponWindow(WeaponDataContainer weaponDataContainer);

        void CloseUpdateWeapon();

        Action<WindowId> OnOpen { get; set; }
        Action<WindowId> OnClose { get; set; }

        void Initialize();

        void Clean();

        bool IsWindowOpened(params WindowId[] ids);
    }
}