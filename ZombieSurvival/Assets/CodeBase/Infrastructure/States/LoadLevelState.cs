using CodeBase.Data.WorldData;
using CodeBase.Infrastructure.Logic;
using CodeBase.Player.Camera;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Levels;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ILogicFactory _logicFactory;
        private readonly IStaticDataService _dataService;
        private readonly IPauseService _pauseService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader, LoadCurtain loadingCurtain, IGameFactory gameFactory, IUIFactory uiFactory, IInputService inputService, IWindowService windowService, IPersistentProgressService persistentProgressService, ILogicFactory logicFactory, IStaticDataService dataService, IPauseService pauseService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
            _logicFactory = logicFactory;
            _dataService = dataService;
            _pauseService = pauseService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            Clean();
            _sceneLoader.Load(_persistentProgressService.PlayerProgress.WorldData.SceneKey, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            string sceneKey = _persistentProgressService.PlayerProgress.WorldData.SceneKey;
            Camera mainCamera = Camera.main;

            InitServices(mainCamera, sceneKey);
            InitWorld(sceneKey, mainCamera);

            _stateMachine.Enter<LoopState>();
        }

        private void InitWorld(string sceneKey, Camera mainCamera)
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD(_windowService);

            InitPlayer();
            InitCamera(mainCamera.gameObject, _gameFactory.Player.transform);
            _uiFactory.CreateMinimap(_gameFactory.Player.transform);
            InitObjectsPiece(_dataService.ForLevel(sceneKey));
            InitQuestPointer();

            InformLoadProgress();
        }

        private void InitServices(Camera mainCamera, string sceneKey)
        {
            _dataService.LoadLevelData(sceneKey);
            _uiFactory.Initialize(mainCamera);
            _windowService.Initialize();
            _gameFactory.Initialize(mainCamera);
            InitLogicFactory();
        }

        private void InitQuestPointer()
        {
            var id = _persistentProgressService.PlayerProgress.WorldData.QuestPointerPieceData.Id;
            _uiFactory.QuestPointerWindow.InitWorldPointer(id);
        }

        private void InitCamera(GameObject mainCamera, Transform player)
        {
            mainCamera.GetComponent<CameraFollow>()?.Construct(player);
            if (mainCamera.TryGetComponent(out CameraLook cameraLook))
            {
                cameraLook.Construct(_inputService, _dataService);
                _gameFactory.PlayerFreezes.Add(cameraLook);
            }
        }

        private void InformLoadProgress()
        {
            _gameFactory.SaveLoads.ForEach(x => x.Load());
        }

        private void InitLogicFactory()
        {
            _logicFactory.InitWeaponSlotsHandler(_persistentProgressService, _gameFactory, _dataService, _uiFactory);
            _logicFactory.InitQuestSlotsHandler(_persistentProgressService, _dataService);
            _logicFactory.InitSwitchPlayerState(_pauseService, _windowService);
            _logicFactory.InitShotEffectPool(_gameFactory);
            _logicFactory.InitQuestPlayer(_dataService, _gameFactory, _windowService);
        }

        private void InitPlayer()
        {
            _gameFactory.CreatePlayer(_persistentProgressService.PlayerProgress.PlayerData.Position, _windowService);
        }

        private void InitObjectsPiece(LevelConfig levelConfig)
        {
            WorldData worldData = _persistentProgressService.PlayerProgress.WorldData;
            worldData.EnemyDataPieceList.ForEach(x => _gameFactory.CreateEnemy(x));
            worldData.NpcDataPieceList.ForEach(x => _gameFactory.CreateNpc(x, _uiFactory, _windowService));
            worldData.WeaponPieceDataList.ForEach(x => _gameFactory.CreateWeaponPiece(x, _uiFactory));
            worldData.QuestItemPieceDataList.ForEach(x => _gameFactory.CreateQuestItem(x, _uiFactory));
            worldData.EnvironmentDataList.ForEach(x => _gameFactory.CreateProps(x));
            levelConfig.SaveZoneList.ForEach(x => _gameFactory.CreateSaveZone(x.Position, x.Rotate, _saveLoadService));
        }

        private void Clean()
        {
            _uiFactory.Clean();
            _windowService.Clean();
            _gameFactory.Clean();
            _inputService.Clean();
            _logicFactory.Clean();
        }
    }
}