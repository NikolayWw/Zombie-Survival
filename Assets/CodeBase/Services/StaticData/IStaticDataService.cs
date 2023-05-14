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
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        WindowConfig ForWindow(WindowId id);

        InventoryWeaponConfig InventoryWeaponConfig { get; }
        Sprite EmptySprite { get; }
        PlayerConfig PlayerConfig { get; }
        AidKitConfig AidKitConfig { get; }
        NpcFindPlayerConfig NpcFindPlayerConfig { get; }
        PropsConfigContainer PropsConfigContainer { get; }
        MinimapConfig MinimapConfig { get; }
        WeaponConfigContainer WeaponConfigContainer { get; }

        BaseWeaponConfig ForWeapon(WeaponId id);

        LevelConfig ForLevel(in string sceneKey);

        QuestItemConfig ForQuestItem(QuestItemId id);

        EnemyConfig ForEnemy(EnemyId id);

        GameObject ForShotEffect(ShotEffectId id);

        DialogContextData ForDialog(DialogId id);

        NpcConfig ForNpc(NpcId id);

        void LoadLevelData(in string sceneKey);

        DialogPointStaticData ForNpcDialogPoint(NpcId id);

        PropsConfig ForProps(PropsId id);

        QuesPointerPositionStaticData ForQuestPointerPosition(QuestPointerId id);
    }
}