using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.Pause;
using CodeBase.Services.Ads;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Dialogs;
using CodeBase.UI.Windows.GameMenu;
using CodeBase.UI.Windows.Inventory;
using CodeBase.UI.Windows.Inventory.QuestInventory;
using CodeBase.UI.Windows.Inventory.WeaponInventory;
using CodeBase.UI.Windows.Inventory.WeaponInventory.Logic;
using CodeBase.UI.Windows.Inventory.WeaponInventory.SlotHandlers;
using CodeBase.UI.Windows.MainMenu;
using CodeBase.UI.Windows.Minimap;
using CodeBase.UI.Windows.Money;
using CodeBase.UI.Windows.PlayerHealth;
using CodeBase.UI.Windows.QuestPointer;
using CodeBase.UI.Windows.Shop;
using CodeBase.UI.Windows.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ILogicFactory _logicFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private readonly IAdsService _adsService;

        public Dictionary<WindowId, BaseWindow> WindowsContainer { get; } = new Dictionary<WindowId, BaseWindow>();
        public Action<WindowId> OnWindowClose { get; set; }
        public List<IFreeze> Freezes { get; } = new List<IFreeze>();
        public QuestPointerWindow QuestPointerWindow { get; private set; }

        private Transform _uiRoot;
        private Camera _mainCamera;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPersistentProgressService persistentProgressService, ILogicFactory logicFactory, IGameFactory gameFactory, ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine, IInputService inputService, IAdsService adsService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;
            _logicFactory = logicFactory;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _adsService = adsService;
        }

        public void Initialize(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Clean()
        {
            WindowsContainer.Clear();
            OnWindowClose = null;
            _mainCamera = null;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetsPath.UIRoot).transform;

        public void CreateHUD(IWindowService windowService)
        {
            GameObject instantiate = _assetProvider.Instantiate(AssetsPath.UIHud, _uiRoot);
            instantiate.GetComponent<OpenWindowButton>()?.Construct(windowService);
            instantiate.GetComponentInChildren<UIWeaponSlotsContainer>()?.Construct(this, _persistentProgressService, windowService);
            instantiate.GetComponentInChildren<UIQuestItemsSlotsContainer>()?.Construct(this, _persistentProgressService, _logicFactory);
            instantiate.GetComponentInChildren<ShowMoneyWindow>()?.Construct(_persistentProgressService);
            instantiate.GetComponentInChildren<UpdatePlayerHealth>()?.Construct(_persistentProgressService, _staticDataService);
            instantiate.GetComponentInChildren<AidKitWindowUpdate>()?.Construct(_persistentProgressService);
            instantiate.GetComponentInChildren<MinimapSetTextureRenderer>()?.Construct(_staticDataService.MinimapConfig.RenderTexture);

            instantiate.GetComponentInChildren<CameraLookButton>()?.Construct(_inputService);

            QuestPointerWindow = instantiate.GetComponentInChildren<QuestPointerWindow>();
            QuestPointerWindow.Construct(this, _staticDataService, _persistentProgressService);
        }

        public void CreateMinimap(Transform followTarget)
        {
            var instance = _assetProvider.Instantiate(AssetsPath.UIMinimap);
            instance.GetComponent<Minimap>()?.Construct(_gameFactory, _staticDataService, followTarget);
        }

        public QuestUIPointerTarget CreateUIQuestPointerTarget(Transform parent) =>
            _assetProvider.Instantiate(AssetsPath.UIQuestPointerTarget, parent).GetComponent<QuestUIPointerTarget>();

        public QuestPointerWorldTarget CreateWorldQuestPointerTarget()
        {
            QuestPointerWorldTarget worldPointer = _assetProvider.Instantiate(AssetsPath.QuestPointerWorldTarget).GetComponent<QuestPointerWorldTarget>();
            return worldPointer;
        }

        public UIQuestItemSlotUpdate CreateQuestItemInventorySlot(Transform parent)
        {
            GameObject instantiate = _assetProvider.Instantiate(AssetsPath.UIQuestItemSlot, parent);
            UIQuestItemSlotUpdate slotUpdate = instantiate.GetComponent<UIQuestItemSlotUpdate>();
            slotUpdate.Construct(_staticDataService);
            return slotUpdate;
        }

        public void CreateWeaponSlot(IWindowService windowService, Transform parent, int slotIndex)
        {
            GameObject instantiate = _assetProvider.Instantiate(AssetsPath.UIInventoryWeaponSlot, parent);
            instantiate.GetComponent<WeaponChangeSelect>()?.Construct(_logicFactory, slotIndex);
            instantiate.GetComponent<UIWeaponSlotUpdate>()?.Construct(_persistentProgressService, _staticDataService, slotIndex);
            instantiate.GetComponent<DropWeapon>()?.Construct(_persistentProgressService, windowService, _logicFactory, slotIndex);
            RegisterFreezePlayer(instantiate);
        }

        public void CreateDropWeaponWindow()
        {
            InstantiateRegister<DropWeaponWindow>(WindowId.DropWeaponWindow);
        }

        public void CreateFirearmUpdateWindow(WeaponDataContainer dataContainer)
        {
            var window = InstantiateRegister<UpdateWeaponAmmoWindow>(WindowId.FirearmUpdateWeaponWindow);
            window.Construct(dataContainer, _staticDataService);
        }

        public void CreateShop(IWindowService windowService)
        {
            ShopWindow shopWindow = InstantiateRegister<ShopWindow>(WindowId.ShopWindow);
            shopWindow.Construct(this, _staticDataService, _persistentProgressService, _adsService);
            shopWindow.GetComponent<CloseWindowButton>()?.Construct(windowService);
            shopWindow.GetComponent<BuyButton>()?.Construct(_staticDataService, _logicFactory, _persistentProgressService);
            shopWindow.GetComponentInChildren<ShopItemDescription>()?.Construct(_staticDataService, _logicFactory, _persistentProgressService);
        }

        public UIShopSlot CreateShopSlot(Transform parent)
        {
            UIShopSlot slot = _assetProvider.Instantiate(AssetsPath.UISlotShop, parent).GetComponent<UIShopSlot>();
            return slot;
        }

        public HoverAtMessage CreateHoverAtMessage(Vector3 at, Transform parent, Transform followTarget)
        {
            GameObject instance = _assetProvider.Instantiate(AssetsPath.UIHoverAtMessage, at, parent);
            instance.GetComponent<Canvas>().worldCamera = _mainCamera;
            HoverAtMessage hoverAtMessage = instance.GetComponent<HoverAtMessage>();
            hoverAtMessage.Construct(_staticDataService);
            instance.GetComponent<LookAtCamera>()?.Construct(_mainCamera);
            instance.GetComponent<FollowTarget>()?.Construct(followTarget);
            return hoverAtMessage;
        }

        public void CreateDialogWindow()
        {
            InstantiateRegister<DialogWindow>(WindowId.DialogWindow);
        }

        public void CreateGameMenu(IWindowService windowService)
        {
            var gameMenu = InstantiateRegister<GameMenuWindow>(WindowId.GameMenuWindow);
            gameMenu.GetComponent<CloseWindowButton>()?.Construct(windowService);
            gameMenu.GetComponent<LoadMainMenuButton>()?.Construct(_gameStateMachine);
        }

        public void CreateMainMenu()
        {
            MainMenuWindow mainMenu = InstantiateRegister<MainMenuWindow>(WindowId.MainMenuWindow);
            mainMenu.Construct(_saveLoadService);
            mainMenu.GetComponent<NewGameButton>()?.Construct(_gameStateMachine, _persistentProgressService, _saveLoadService);
            mainMenu.GetComponent<ResumeButton>()?.Construct(_gameStateMachine, _persistentProgressService, _saveLoadService);
        }

        private void RegisterFreezePlayer(GameObject gameObject)
        {
            foreach (IFreeze freeze in gameObject.GetComponentsInChildren<IFreeze>())
                Freezes.Add(freeze);
        }

        private TWindow InstantiateRegister<TWindow>(WindowId id) where TWindow : BaseWindow
        {
            WindowConfig config = _staticDataService.ForWindow(id);
            var window = Object.Instantiate(config.Template, _uiRoot);

            window.SetId(id);
            window.OnClosed += SendOnClosed;
            WindowsContainer.Add(id, window);
            return (TWindow)window;
        }

        private void SendOnClosed(WindowId id) =>
            OnWindowClose?.Invoke(id);
    }
}