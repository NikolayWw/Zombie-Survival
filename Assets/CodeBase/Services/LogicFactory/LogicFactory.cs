using CodeBase.Infrastructure.Logic;
using CodeBase.Logic;
using CodeBase.Logic.Inventory.QuestItemInventory;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Logic.ObjectPool;
using CodeBase.Logic.Pause;
using CodeBase.Logic.Quest;
using CodeBase.Services.Factory;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;

namespace CodeBase.Services.LogicFactory
{
    public class LogicFactory : ILogicFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public WeaponSlotsHandler WeaponSlotsHandler { get; private set; }
        public QuestSlotsHandler QuestSlotsHandler { get; private set; }
        public ShotEffectPool ShotEffectPool { get; private set; }
        public QuestPlayer QuestPlayer { get; private set; }

        private SwitchPlayerState _switchPlayerState;
        private LockCursor _lockCursor;

        public LogicFactory(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void InitWeaponSlotsHandler(IPersistentProgressService persistentProgressService, IGameFactory gameFactory, IStaticDataService dataService, IUIFactory uiFactory) =>
            WeaponSlotsHandler = new WeaponSlotsHandler(persistentProgressService, gameFactory, uiFactory);

        public void InitQuestSlotsHandler(IPersistentProgressService persistentProgressService, IStaticDataService dataService) =>
            QuestSlotsHandler = new QuestSlotsHandler(persistentProgressService, dataService);

        public void InitSwitchPlayerState(IPauseService pauseService, IWindowService windowService) =>
            _switchPlayerState = new SwitchPlayerState(pauseService, windowService);

        public void InitShotEffectPool(IGameFactory gameFactory) =>
            ShotEffectPool = new ShotEffectPool(gameFactory);

        public void InitQuestPlayer(IStaticDataService dataService, IGameFactory gameFactory, IWindowService windowService) =>
            QuestPlayer = new QuestPlayer(_coroutineRunner, dataService, gameFactory, windowService);

        public void InitLockCursor(IWindowService windowService)
        {
            _lockCursor = new LockCursor(windowService);
        }

        public void Clean()
        {
            WeaponSlotsHandler = null;
            QuestSlotsHandler = null;
            _switchPlayerState = null;
            ShotEffectPool = null;
            QuestPlayer = null;

            _lockCursor?.Clenup();
            _lockCursor = null;
        }
    }
}