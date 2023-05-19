using CodeBase.Data.Props;
using CodeBase.Data.WorldData;
using CodeBase.Logic.ObjectPool;
using CodeBase.Logic.Pause;
using CodeBase.Logic.Quest;
using CodeBase.Player;
using CodeBase.Player.PlayerAnimation;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using CodeBase.StaticData.Props;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.Minimap;
using CodeBase.Weapon;
using CodeBase.Weapon.GravityGun;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePlayer(Vector3 at, IWindowService windowService);
        void Initialize(Camera mainCamera);
        void CreateWeaponPiece(WeaponPieceData pieceData, IUIFactory uiFactory);
        GameObject Player { get; }
        List<IFreeze> PlayerFreezes { get; }
        List<ISaveLoad> SaveLoads { get; }
        List<Collider> PlayerColliders { get; }
        List<WorldMinimapIcon> MinimapIcons { get; }
        List<IPause> Pauses { get; }
        void Clean();
        FXShotObject CreateFXShot(ShotEffectId id);
        QuestAudioPlayer CreateQuestAudioPlayer();
        GameObject CreateNpc(NpcPieceData pieceData, IUIFactory uiFactory, IWindowService windowService);
        GameObject CreateEnemy(EnemyPieceData pieceData);
        void CreateQuestItem(QuestItemPieceData pieceData, IUIFactory uiFactory);
        GravityGunAnchor CreateGravityGunAnchor();
        void CreateProps(PropsPieceData pieceData);
        void CreatePropsFX(PropsId propsId, Vector3 at);
        void CreateSaveZone(Vector3 at, Quaternion rotate, ISaveLoadService saveLoad);
        BaseWeaponAttackHandler CreateWeaponInHand(WeaponDataContainer weaponDataContainer, Transform parent, PlayerAnimator playerAnimator, PlayerAudioPlayer audioPlayer, PlayerAnchors playerAnchors, IWindowService windowService);
    }
}