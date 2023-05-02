using CodeBase.Services.Pause;
using CodeBase.UI.Services.Window;

namespace CodeBase.Logic.Pause
{
    public class SwitchPlayerState
    {
        private readonly IPauseService _pauseService;
        private readonly IWindowService _windowService;

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
            if (WindowId.GameMenuWindow == id)
                _pauseService.Pause();
        }

        private void UnPause(WindowId id)
        {
            if (WindowId.GameMenuWindow == id)
                _pauseService.Play();
        }

        private void Freeze(WindowId id)
        {
            if (WindowId.ShopWindow == id)
                _pauseService.FreezePlayer();
        }

        private void Unfreeze(WindowId id)
        {
            if (WindowId.ShopWindow == id)
                _pauseService.UnfreezePlayer();
        }
    }
}