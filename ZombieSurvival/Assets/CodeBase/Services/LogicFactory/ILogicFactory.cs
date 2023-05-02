using CodeBase.Logic.Inventory.QuestItemInventory;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Logic.ObjectPool;
using CodeBase.Logic.Quest;
using CodeBase.Services.Factory;
using CodeBase.Services.Pause;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;

namespace CodeBase.Services.LogicFactory
{
    public interface ILogicFactory : IService
    {
        WeaponSlotsHandler WeaponSlotsHandler { get; }
        QuestSlotsHandler QuestSlotsHandler { get; }
        ShotEffectPool ShotEffectPool { get; }
        QuestPlayer QuestPlayer { get; }

        void Clean();

        void InitWeaponSlotsHandler(IPersistentProgressService persistentProgressService, IGameFactory gameFactory, IStaticDataService dataService, IUIFactory uiFactory);

        void InitSwitchPlayerState(IPauseService pauseService, IWindowService windowService);

        void InitQuestSlotsHandler(IPersistentProgressService persistentProgressService, IStaticDataService dataService);

        void InitShotEffectPool(IGameFactory gameFactory);

        void InitQuestPlayer(IStaticDataService dataService, IGameFactory gameFactory, IWindowService windowService);
    }
}