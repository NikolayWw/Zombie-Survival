using CodeBase.Services.Pause;
using CodeBase.UI.Services.Window;
using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.Pause
{
    public class SwitchPlayerState
    {
        private readonly WindowId[] PauseWindows = { WindowId.GameMenuWindow };
        private readonly WindowId[] FreezeWindows = { WindowId.ShopWindow };

        private readonly IPauseService _pauseService;
        private readonly IWindowService _windowService;

        private bool _isPause;
        private bool _isPlayerFreeze;

        public SwitchPlayerState(IPauseService pauseService, IWindowService windowService)
        {
            _pauseService = pauseService;
            _windowService = windowService;

            _windowService.OnOpen += Freeze;
            _windowService.OnOpen += Pause;
            _windowService.OnClose += Unfreeze;
            _windowService.OnClose += UnPause;
        }

        private void Pause(WindowId id)
        {
            if (_isPause)
                return;

            if (PauseWindows.Contains(id))
            {
                _pauseService.Pause();
                _isPause = true;
            }
        }

        private void UnPause(WindowId id)
        {
            if (_isPause == false)
                return;

            if (_windowService.IsWindowOpened(PauseWindows) == false)
            {
                _pauseService.Play();
                _isPause = false;
            }
        }

        private void Freeze(WindowId id)
        {
            if (_isPlayerFreeze)
                return;

            if (FreezeWindows.Contains(id))
            {
                _pauseService.FreezePlayer();
                _isPlayerFreeze = true;
            }
        }

        private void Unfreeze(WindowId id)
        {
            if (_isPlayerFreeze == false)
                return;

            if (_windowService.IsWindowOpened(FreezeWindows) == false)
            {
                _pauseService.UnfreezePlayer();
                _isPlayerFreeze = false;
            }
        }
    }
}