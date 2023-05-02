using CodeBase.Services.Factory;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public PauseService(IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Play()
        {
            _gameFactory.Pauses.ForEach(x => x.Play());
        }

        public void Pause()
        {
            foreach (var pause in _gameFactory.Pauses) 
                pause.Pause();

        }

        public void FreezePlayer()
        {
            _gameFactory.PlayerFreezes.ForEach(x => x.Freeze());
            _uiFactory.Freezes.ForEach(x => x.Freeze());
        }

        public void UnfreezePlayer()
        {
            _gameFactory.PlayerFreezes.ForEach(x => x.Unfreeze());
            _uiFactory.Freezes.ForEach(x => x.Unfreeze());
        }
    }
}