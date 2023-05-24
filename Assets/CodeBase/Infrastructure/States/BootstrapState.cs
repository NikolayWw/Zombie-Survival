using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using Unity.Services.Analytics;
using Unity.Services.Core;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(GameConstants.InitialSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            InitializeGamingServicesAsync();
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterStaticData();
            RegisterAds();
            _services.RegisterSingle<IInputService>(new InputService());

            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<ILogicFactory>(new LogicFactory(_coroutineRunner));
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<ILogicFactory>(),
                _services.Single<IInputService>(),
                _services.Single<IPersistentProgressService>()));

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>(),
                _services.Single<IStaticDataService>()));

            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<ILogicFactory>(),
                _services.Single<IGameFactory>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IGameStateMachine>(),
                _services.Single<IInputService>(),
                _services.Single<IAdsService>()));

            _services.RegisterSingle<IPauseService>(new PauseService(_services.Single<IGameFactory>(), _services.Single<IUIFactory>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadMainMenuState>();
        }

        private async void InitializeGamingServicesAsync()
        {
            await UnityServices.InitializeAsync();
            await AnalyticsService.Instance.CheckForRequiredConsents();
        }

        private void RegisterAds()
        {
            var adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
        }

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}