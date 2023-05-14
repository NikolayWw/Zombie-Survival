using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States
{
    public class LoadMainMenuState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;

        public LoadMainMenuState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadCurtain loadCurtain, IUIFactory uiFactory, IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _loadCurtain = loadCurtain;
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
        }

        public void Enter()
        {
            _loadCurtain.Show();
            _sceneLoader.Load(GameConstants.MainMenuSceneKey, OnLoaded);
        }

        public void Exit()
        {
            _loadCurtain.Hide();
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowId.MainMenuWindow);
            _gameStateMachine.Enter<LoopState>();
        }
    }
}