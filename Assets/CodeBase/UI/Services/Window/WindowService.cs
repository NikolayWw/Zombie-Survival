using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using System;
using UnityEngine;

namespace CodeBase.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        public Action<WindowId> OnOpen { get; set; }
        public Action<WindowId> OnClose { get; set; }

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Clean()
        {
            _uiFactory.OnWindowClose -= RemoveInContainer;
            OnOpen = null;
            OnClose = null;
        }

        public void Initialize()
        {
            _uiFactory.OnWindowClose += RemoveInContainer;
        }

        public void OpenUpdateWeaponWindow(WeaponDataContainer weaponDataContainer)
        {
            switch (weaponDataContainer.GetData())
            {
                case FirearmWeaponData _:
                    _uiFactory.CreateFirearmUpdateWindow(weaponDataContainer);
                    break;
            }
        }

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.None:
                    break;

                case WindowId.DropWeaponWindow:
                    _uiFactory.CreateDropWeaponWindow();
                    break;

                case WindowId.FirearmUpdateWeaponWindow:
                    Debug.LogError("need another method overload");
                    break;

                case WindowId.ShopWindow:
                    _uiFactory.CreateShop(this);
                    break;

                case WindowId.DialogWindow:
                    _uiFactory.CreateDialogWindow();
                    break;

                case WindowId.GameMenuWindow:
                    _uiFactory.CreateGameMenu(this);
                    break;

                case WindowId.MainMenuWindow:
                    _uiFactory.CreateMainMenu();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
            OnOpen?.Invoke(id);
        }

        public void Close(WindowId id)
        {
            if (GetWindow(id, out BaseWindow window))
                window.Close();
            OnClose?.Invoke(id);
        }

        public void CloseUpdateWeapon()
        {
            Close(WindowId.FirearmUpdateWeaponWindow);
        }

        public bool GetWindow<TWindow>(WindowId id, out TWindow window) where TWindow : BaseWindow
        {
            window = _uiFactory.WindowsContainer.TryGetValue(id, out var valueWindow) ? (TWindow)valueWindow : null;
            return window;
        }

        public bool IsWindowOpened(params WindowId[] ids)
        {
            foreach (WindowId id in ids)
                if (_uiFactory.WindowsContainer.ContainsKey(id))
                    return true;

            return false;
        }

        private void RemoveInContainer(WindowId id) =>
            _uiFactory.WindowsContainer.Remove(id);
    }
}