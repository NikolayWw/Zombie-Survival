using CodeBase.Logic.Pause;
using CodeBase.Services;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Inventory.QuestInventory;
using CodeBase.UI.Windows.QuestPointer;
using CodeBase.UI.Windows.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();

        Dictionary<WindowId, BaseWindow> WindowsContainer { get; }
        Action<WindowId> OnWindowClose { get; set; }
        List<IFreeze> Freezes { get; }
        QuestPointerWindow QuestPointerWindow { get; }

        void Clean();

        void CreateWeaponSlot(IWindowService windowService, Transform parent, int index);

        void CreateDropWeaponWindow();

        void CreateFirearmUpdateWindow(WeaponDataContainer data);

        void CreateShop(IWindowService windowService);

        HoverAtMessage CreateHoverAtMessage(Vector3 at, Transform parent, Transform followTarget);

        void Initialize(Camera mainCamera);

        void CreateHUD(IWindowService windowService);

        UIQuestItemSlotUpdate CreateQuestItemInventorySlot(Transform parent);

        UIShopSlot CreateShopSlot(Transform parent);

        void CreateDialogWindow();

        void CreateGameMenu(IWindowService windowService);

        void CreateMinimap(Transform followTarget);

        QuestUIPointerTarget CreateUIQuestPointerTarget(Transform parent);

        QuestPointerWorldTarget CreateWorldQuestPointerTarget();

        void CreateMainMenu();
    }
}