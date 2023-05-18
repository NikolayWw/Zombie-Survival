using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LockCursor
    {
        private readonly WindowId[] WhiteListChangeState = { WindowId.GameMenuWindow, WindowId.ShopWindow };
        private readonly IWindowService _windowService;

        public LockCursor(IWindowService windowService)
        {
            _windowService = windowService;

            Cursor.lockState = CursorLockMode.Locked;

            _windowService.OnOpen += Unlock;
            _windowService.OnClose += Lock;
        }

        public void Clenup()
        {
            _windowService.OnOpen -= Unlock;
            _windowService.OnClose -= Lock;
        }

        private void Lock(WindowId id)
        {
            foreach (WindowId whiteId in WhiteListChangeState)
                if (whiteId == id)
                    if (_windowService.IsWindowOpened(WhiteListChangeState) == false)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        break;
                    }
        }

        private void Unlock(WindowId id)
        {
            foreach (WindowId whiteId in WhiteListChangeState)
                if (whiteId == id)
                {
                    Cursor.lockState = CursorLockMode.None;
                    break;
                }
        }
    }
}