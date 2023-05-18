using CodeBase.BehaviourTree.Behaviour;
using CodeBase.Data.Props;
using CodeBase.Data.WorldData;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Logic.ObjectPool;
using CodeBase.Logic.Pause;
using CodeBase.Logic.Quest;
using CodeBase.Npc;
using CodeBase.Npc.Dialogs;
using CodeBase.Player;
using CodeBase.Player.Move;
using CodeBase.Player.PlayerAnimation;
using CodeBase.Props;
using CodeBase.QuestItems;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using CodeBase.StaticData.Minimap;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Props;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.Minimap;
using CodeBase.Weapon;
using CodeBase.Weapon.Firearms;
using CodeBase.Weapon.GravityGun;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _dataService;
        private readonly ILogicFactory _logicFactory;
        private readonly IInputService _inputService;
        private readonly IPersistentProgressService _persistentProgressService;

        private Camera _mainCamera;
        public List<ISaveLoad> SaveLoads { get; } = new List<ISaveLoad>();
        public List<IPause> Pauses { get; } = new List<IPause>();
        public List<IFreeze> PlayerFreezes { get; } = new List<IFreeze>();
        public List<Collider> PlayerColliders { get; } = new List<Collider>();
        public List<WorldMinimapIcon> MinimapIcons { get; } = new List<WorldMinimapIcon>();
        public GameObject Player { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService dataService, ILogicFactory logicFactory, IInputService inputService, IPersistentProgressService persistentProgressService)
        {
            _assetProvider = assetProvider;
            _dataService = dataService;
            _logicFactory = logicFactory;
            _inputService = inputService;
            _persistentProgressService = persistentProgressService;
        }

        public void Initialize(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Clean()
        {
            SaveLoads.Clear();
            Pauses.Clear();
            PlayerFreezes.Clear();
            PlayerColliders.Clear();
            MinimapIcons.Clear();
        }

        public void CreateWeaponPiece(WeaponPieceData pieceData, IUIFactory uiFactory)
        {
            var dataContainer = pieceData.WeaponDataContainer;
            if (dataContainer.GetData() == null && dataContainer.GetData().WeaponId == WeaponId.None)
                return;

            CheckerListPresentPieceData(_persistentProgressService.PlayerProgress.WorldData.WeaponPieceDataList, pieceData);
            BaseWeaponConfig config = _dataService.ForWeapon(dataContainer.GetData().WeaponId);
            GameObject instantiate = InstantiateRegister(config.PrefabInPiece, pieceData.Position);
            instantiate.GetComponent<WeaponInteraction>()?.Construct(pieceData, _logicFactory, uiFactory, _persistentProgressService);
        }

        public void CreateSaveZone(Vector3 at, Quaternion rotate, ISaveLoadService saveLoad)
        {
            GameObject instance = _assetProvider.Instantiate(AssetsPath.SaveZone, at, rotate);
            instance.GetComponent<SaveZone>()?.Construct(saveLoad, _mainCamera);
        }

        public BaseWeaponAttackHandler CreateWeaponInHand(WeaponDataContainer weaponDataContainer, Transform parent, PlayerAnimator playerAnimator, PlayerAudioPlayer audioPlayer, PlayerAnchors playerAnchors, IWindowService windowService)
        {
            BaseWeaponConfig config = _dataService.ForWeapon(weaponDataContainer.GetData().WeaponId);
            GameObject weapon = InstantiateRegister(config.PrefabInHand, parent);
            BaseWeaponAttackHandler attackHandler = weapon.GetComponent<BaseWeaponAttackHandler>();
            attackHandler.Construct(weaponDataContainer, _dataService, _inputService, _mainCamera, playerAnimator, audioPlayer);

            switch (attackHandler)
            {
                case FirearmsWeaponAttackHandler handler:
                    handler.Construct();
                    break;

                case MeleeWeaponAttackHandler handler:
                    handler.Construct();
                    break;

                case GravityGunAttackHandler handler:
                    handler.Construct(this);
                    break;

                default:
                    Debug.LogError("Handler not found");
                    break;
            }
            weapon.GetComponent<WeaponHitEffect>()?.Construct(config, _logicFactory);

            switch (attackHandler)
            {
                case FirearmsWeaponAttackHandler _:
                    weapon.GetComponent<FirearmsWeaponAnimator>()?.Construct(playerAnchors, playerAnimator);
                    break;
            }

            RegisterPlayerFreeze(weapon);
            return attackHandler;
        }

        public GravityGunAnchor CreateGravityGunAnchor()
        {
            GravityGunAnchor instantiate = _assetProvider.Instantiate(AssetsPath.GravityGunConnectAnchor, _mainCamera.transform).GetComponent<GravityGunAnchor>();
            return instantiate;
        }

        public void CreatePlayer(Vector3 at, IWindowService windowService)
        {
            var instance = InstantiateRegister(AssetsPath.Player, at);
            instance.GetComponent<PlayerChangeWeapon>()?.Construct(this, windowService, _logicFactory);
            instance.GetComponent<MoveStateMachine>()?.Construct(_inputService, _mainCamera, _dataService, _persistentProgressService);
            instance.GetComponent<SwitchMoveState>()?.Construct(_dataService);
            instance.GetComponent<PlayerApplyDamage>()?.Construct(_persistentProgressService);
            instance.GetComponent<PlayerUseAidKit>()?.Construct(_inputService, _persistentProgressService, _dataService);
            instance.GetComponent<PlayerInteraction>()?.Construct(_dataService, _inputService, _mainCamera);
            instance.GetComponent<PlayerChangeWeaponKeyboard>()?.Construct(_logicFactory, _inputService);
            instance.GetComponentInChildren<PlayerRotateBody>()?.Construct(_mainCamera, _dataService);
            instance.GetComponentInChildren<SpinningBehindCamera>()?.Construct(_mainCamera);

            RegisterMinimapIcon(_dataService.PlayerConfig.MinimapWorldIconConfig, instance.GetComponent<WorldMinimapIcon>());
            RegisterPlayerFreeze(instance);
            RegisterPlayerColliders(instance);

            Player = instance;
        }

        public GameObject CreateEnemy(EnemyPieceData pieceData)
        {
            CheckerListPresentPieceData(_persistentProgressService.PlayerProgress.WorldData.EnemyDataPieceList, pieceData);
            EnemyConfig config = _dataService.ForEnemy(pieceData.EnemyId);
            GameObject instance = InstantiateRegister(config.Prefab, pieceData.Position);

            instance.GetComponent<EnemyPiece>()?.Construct(_persistentProgressService, pieceData);
            instance.GetComponent<EnemyBehaviour>()?.Construct(pieceData, _dataService);
            instance.GetComponentInChildren<EnemyApplyDamage>()?.Construct(pieceData, _dataService);

            return instance;
        }

        public GameObject CreateNpc(NpcPieceData pieceData, IUIFactory uiFactory, IWindowService windowService)
        {
            CheckerListPresentPieceData(_persistentProgressService.PlayerProgress.WorldData.NpcDataPieceList, pieceData);
            NpcConfig config = _dataService.ForNpc(pieceData.NpcId);
            GameObject instance = InstantiateRegister(config.Prefab, pieceData.Position);

            if (instance.TryGetComponent(out BaseDialog baseDialog))
            {
                baseDialog.BaseConstruct(_persistentProgressService, uiFactory, _logicFactory);
            }
            instance.transform.rotation = pieceData.Rotate;
            instance.GetComponent<OpenShop>()?.Construct(windowService, _dataService, pieceData.NpcId);
            instance.GetComponent<NpcFindPlayerQuestReporter>()?.Construct(_dataService, pieceData.NpcId);
            RegisterMinimapIcon(config.IconConfig, instance.GetComponent<WorldMinimapIcon>());
            return instance;
        }

        public void CreateQuestItem(QuestItemPieceData pieceData, IUIFactory uiFactory)
        {
            if (pieceData.ItemId == QuestItemId.None)
                return;

            CheckerListPresentPieceData(_persistentProgressService.PlayerProgress.WorldData.QuestItemPieceDataList, pieceData);
            QuestItemConfig config = _dataService.ForQuestItem(pieceData.ItemId);
            GameObject instance = InstantiateRegister(config.PrefabPiece, pieceData.Position);
            instance.GetComponent<QuestItemInteraction>()?.Construct(_persistentProgressService, pieceData, uiFactory, _logicFactory);
        }

        public FXShotObject CreateFXShot(ShotEffectId id)
        {
            GameObject prefab = _dataService.ForShotEffect(id);
            return Object.Instantiate(prefab).GetComponent<FXShotObject>();
        }

        public QuestAudioPlayer CreateQuestAudioPlayer()
        {
            QuestAudioPlayer asd = InstantiateRegister(AssetsPath.QuestAudioPlayer).GetComponent<QuestAudioPlayer>();
            asd.Construct(this);
            return asd;
        }

        public void CreateProps(PropsPieceData pieceData)
        {
            if (pieceData.PropsId == PropsId.None)
                return;

            CheckerListPresentPieceData(_persistentProgressService.PlayerProgress.WorldData.EnvironmentDataList, pieceData);
            var config = _dataService.ForProps(pieceData.PropsId);
            GameObject instance = InstantiateRegister(config.PrefabInPiece, pieceData.Position);
            instance.GetComponent<PropsPiece>()?.Construct(pieceData, this, _persistentProgressService);
            instance.GetComponent<PropsApplyDamage>()?.Construct(pieceData, this, _dataService);
        }

        public void CreatePropsFX(PropsId propsId, Vector3 at)
        {
            PropsConfig config = _dataService.ForProps(propsId);
            foreach (var fxPrefab in config.PropsFxPrefabs)
            {
                var instance = Object.Instantiate(fxPrefab, at, Random.rotation);
                if (instance.TryGetComponent(out PropsFX fx))
                    fx.Construct(_dataService.PropsConfigContainer.LifeTimeFx, Random.insideUnitSphere.normalized * _dataService.PropsConfigContainer.FxSpreadForce);
            }
        }

        private static void CheckerListPresentPieceData<TList>(List<TList> pieceDataList, TList data)
        {
            if (pieceDataList.Contains(data) == false)
                pieceDataList.Add(data);
        }

        #region Register

        private GameObject InstantiateRegister(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            Register(instance);
            return instance;
        }

        private GameObject InstantiateRegister(GameObject prefab, Vector3 at)
        {
            var instance = Object.Instantiate(prefab, at, Quaternion.identity);
            Register(instance);
            return instance;
        }

        private GameObject InstantiateRegister(string path)
        {
            var instance = _assetProvider.Instantiate(path);
            Register(instance);
            return instance;
        }

        private GameObject InstantiateRegister(string path, Vector3 at)
        {
            var instance = _assetProvider.Instantiate(path, at, Quaternion.identity);
            Register(instance);
            return instance;
        }

        private void RegisterPlayerFreeze(GameObject instance)
        {
            foreach (IFreeze freeze in instance.GetComponentsInChildren<IFreeze>())
                PlayerFreezes.Add(freeze);
        }

        private void RegisterPlayerColliders(GameObject player)
        {
            foreach (Collider collider in player.GetComponentsInChildren<Collider>())
                PlayerColliders.Add(collider);
        }

        private void RegisterMinimapIcon(MinimapWorldIconConfig iconConfig, WorldMinimapIcon worldIcon)
        {
            if (worldIcon == null)
                return;

            worldIcon.Construct(iconConfig, this);
            MinimapIcons.Add(worldIcon);
        }

        private void Register(GameObject gameObject)
        {
            foreach (var child in gameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                if (child is ISaveLoad saveLoad)
                    SaveLoads.Add(saveLoad);
                if (child is IPause pause)
                    Pauses.Add(pause);
            }
        }

        #endregion Register
    }
}