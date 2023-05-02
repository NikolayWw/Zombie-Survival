using CodeBase.Data;
using CodeBase.Data.Props;
using CodeBase.Data.WeaponInventory;
using CodeBase.Data.WorldData;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Levels;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _dataService;

        public SaveLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory, IStaticDataService dataService)
        {
            _persistentProgressService = persistentProgressService;
            _gameFactory = gameFactory;
            _dataService = dataService;
        }

        public void SaveProgress()
        {
            foreach (ISaveLoad saveLoad in _gameFactory.SaveLoads)
                if (saveLoad.CanSave == false)
                {
                    Debug.Log("Prohibited during the mission");
                    return;
                }

            foreach (ISaveLoad saveLoad in _gameFactory.SaveLoads)
                saveLoad.Save();

            var json = JsonUtility.ToJson(_persistentProgressService.PlayerProgress);
            PlayerPrefs.SetString(GameConstants.SaveProgressKey, json);

            Debug.Log("Game Saved");
        }

        public PlayerProgress LoadProgress()
        {
            string progress = PlayerPrefs.GetString(GameConstants.SaveProgressKey);
            return JsonUtility.FromJson<PlayerProgress>(progress);
        }

        public PlayerProgress NewProgress()
        {
            LevelConfig levelConfig = _dataService.ForLevel(GameConstants.MainSceneKey);
            return new PlayerProgress
            (
                new PlayerData
                (
                    levelConfig.PlayerInitialPosition,
                    _dataService.PlayerConfig.Money,
                    _dataService.PlayerConfig.MaxHealth,
                    _dataService.PlayerConfig.AidKitAmount
                ),
                new WorldData
                (
                    GameConstants.MainSceneKey,
                    levelConfig.WeaponsSpawnStaticData.Select(x => new WeaponPieceData(x.WeaponDataContainer, x.Position)).ToList(),
                    levelConfig.EnemySpawnStaticDataList.Select(x => new EnemyPieceData(x.EnemyId, x.Position, _dataService.ForEnemy(x.EnemyId).MaxHealth)).ToList(),
                    levelConfig.NpcSpawnStaticDataList.Select(x => new NpcPieceData(x.NpcId, x.Position, x.Rotation)).ToList(),
                    levelConfig.QuestItemsStaticData.Select(x => new QuestItemPieceData(x.Id, x.Amount, x.Position)).ToList(),
                    levelConfig.PropsSpawnStaticDataList.Select(x => new PropsPieceData(x.PropsId, x.Position, _dataService.ForProps(x.PropsId).MaxHealth)).ToList(),
                    new QuestPointerPieceData(levelConfig.QuestPointerInitialId)
                ),
                new WeaponSlotsContainer(_dataService.InventoryWeaponConfig)
            );
        }
    }
}