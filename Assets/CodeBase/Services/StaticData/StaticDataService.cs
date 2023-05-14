using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Inventory;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.Minimap;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Props;
using CodeBase.StaticData.QuestPointer;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Window;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        #region Paths

        private const string WindowConfigsContainerPath = "StaticData/UI/WindowConfigContainer";
        private const string PlayerConfigPath = "StaticData/Player/PlayerConfig";
        private const string InventoryWeaponConfigContainerPath = "StaticData/Inventory/InventoryWeaponConfig";
        private const string WeaponConfigContainerPath = "StaticData/Items/Weapon/WeaponConfigContainer";
        private const string QuestItemConfigContainerPath = "StaticData/Items/QuestItems/QuestItemConfigContainer";
        private const string EnemyConfigContainerPath = "StaticData/NPC/Enemy/EnemyConfigsContainer";
        private const string LevelConfigsContainerPath = "StaticData/Level/LevelDataContainer";
        private const string EmptySpritePath = "UI/Elements/EmptyIcon";
        private const string ShotEffectConfigsContainerPath = "StaticData/Items/Weapon/ShotEffectContainer";
        private const string AidKitConfigPath = "StaticData/Items/AidKitConfig";
        private const string DialogConfigsContainerPath = "StaticData/NPC/Npc/NpcDialogsContainer";
        private const string NpcConfigsContainerPath = "StaticData/NPC/Npc/NpcConfigsContainer";
        private const string PropsConfigsContainerPath = "StaticData/Props/PropsConfigContainer";
        private const string MinimapConfigPath = "StaticData/UI/Minimap/MinimapConfig";

        #endregion Paths

        public InventoryWeaponConfig InventoryWeaponConfig { get; private set; }
        public MinimapConfig MinimapConfig { get; private set; }
        public WeaponConfigContainer WeaponConfigContainer { get; private set; }
        public Sprite EmptySprite { get; private set; }
        public AidKitConfig AidKitConfig { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        public NpcFindPlayerConfig NpcFindPlayerConfig { get; private set; }
        public PropsConfigContainer PropsConfigContainer { get; private set; }
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<string, LevelConfig> _levelsStaticData;
        private Dictionary<WeaponId, BaseWeaponConfig> _weaponConfigs;
        private Dictionary<QuestItemId, QuestItemConfig> _questItemConfigs;
        private Dictionary<EnemyId, EnemyConfig> _enemyConfigs;
        private Dictionary<ShotEffectId, GameObject> _shotEffectConfigs;
        private Dictionary<DialogId, DialogContextData> _dialogConfigs;
        private Dictionary<NpcId, NpcConfig> _ncpConfigs;
        private Dictionary<NpcId, DialogPointStaticData> _dialogPoints;
        private Dictionary<PropsId, PropsConfig> _propsConfigs;
        private Dictionary<QuestPointerId, QuesPointerPositionStaticData> _quesPointersStaticData;

        public void Load()
        {
            _windowConfigs = Resources.Load<WindowConfigsContainer>(WindowConfigsContainerPath).Configs.ToDictionary(x => x.WindowId, x => x);
            _levelsStaticData = Resources.Load<LevelsStaticDataContainer>(LevelConfigsContainerPath).LevelsStaticData.ToDictionary(x => x.SceneKey, x => x);
            PlayerConfig = Resources.Load<PlayerConfig>(PlayerConfigPath);
            LoadWeaponConfigs(Resources.Load<WeaponConfigContainer>(WeaponConfigContainerPath));
            _questItemConfigs = Resources.Load<QuesItemConfigContainer>(QuestItemConfigContainerPath).ItemConfigs.ToDictionary(x => x.QuestItemId, x => x);
            InventoryWeaponConfig = Resources.Load<InventoryWeaponConfig>(InventoryWeaponConfigContainerPath);
            EmptySprite = Resources.Load<Sprite>(EmptySpritePath);
            _enemyConfigs = Resources.Load<EnemyConfigsContainer>(EnemyConfigContainerPath).EnemyConfigs.ToDictionary(x => x.EnemyId, x => x);
            _shotEffectConfigs = Resources.Load<ShotEffectConfigsContainer>(ShotEffectConfigsContainerPath).Configs.ToDictionary(x => x.ShotEffectId, x => x.Prefab);
            AidKitConfig = Resources.Load<AidKitConfig>(AidKitConfigPath);
            _dialogConfigs = Resources.Load<NpcDialogsContainer>(DialogConfigsContainerPath).Dialogs.ToDictionary(x => x.DialogId, x => x);
            LoadNpcConfigs();
            LoadPropsConfigs();
            MinimapConfig = Resources.Load<MinimapConfig>(MinimapConfigPath);
        }

        public void LoadLevelData(in string sceneKey)
        {
            _dialogPoints?.Clear();
            _dialogPoints = ForLevel(sceneKey).DialogPointStaticDataList.ToDictionary(x => x.QuestPointId, x => x);

            _quesPointersStaticData?.Clear();
            _quesPointersStaticData = ForLevel(sceneKey).QuesPointerPositionStaticDataList.ToDictionary(x => x.Id, x => x);
        }

        public WindowConfig ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out var data) ? data : null;

        public LevelConfig ForLevel(in string sceneKey) =>
            _levelsStaticData.TryGetValue(sceneKey, out var data) ? data : null;

        public BaseWeaponConfig ForWeapon(WeaponId id) =>
            _weaponConfigs.TryGetValue(id, out var config) ? config : null;

        public QuestItemConfig ForQuestItem(QuestItemId id) =>
            _questItemConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public EnemyConfig ForEnemy(EnemyId id) =>
            _enemyConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public GameObject ForShotEffect(ShotEffectId id) =>
            _shotEffectConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public DialogContextData ForDialog(DialogId id) =>
            _dialogConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public NpcConfig ForNpc(NpcId id) =>
            _ncpConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public DialogPointStaticData ForNpcDialogPoint(NpcId id) =>
            _dialogPoints.TryGetValue(id, out var data) ? data : null;

        public PropsConfig ForProps(PropsId id) =>
            _propsConfigs.TryGetValue(id, out var cfg) ? cfg : null;

        public QuesPointerPositionStaticData ForQuestPointerPosition(QuestPointerId id) =>
            _quesPointersStaticData.TryGetValue(id, out var data) ? data : null;

        private void LoadWeaponConfigs(WeaponConfigContainer weaponConfigContainer)
        {
            WeaponConfigContainer = weaponConfigContainer;
            _weaponConfigs = new Dictionary<WeaponId, BaseWeaponConfig>(weaponConfigContainer.Firearms.Count);
            weaponConfigContainer.Melee.ForEach(x => _weaponConfigs.Add(x.WeaponId, x));
            weaponConfigContainer.Firearms.ForEach(x => _weaponConfigs.Add(x.WeaponId, x));
            weaponConfigContainer.GravityGuns.ForEach(x => _weaponConfigs.Add(x.WeaponId, x));
        }

        private void LoadNpcConfigs()
        {
            var npcConfigsContainer = Resources.Load<NpcConfigsContainer>(NpcConfigsContainerPath);
            NpcFindPlayerConfig = npcConfigsContainer.NpcFindPlayerConfig;
            _ncpConfigs = npcConfigsContainer.NpcConfigs.ToDictionary(x => x.NpcId, x => x);
        }

        private void LoadPropsConfigs()
        {
            PropsConfigContainer = Resources.Load<PropsConfigContainer>(PropsConfigsContainerPath);
            _propsConfigs = PropsConfigContainer.Configs.ToDictionary(x => x.PropsId, x => x);
        }
    }
}