using CodeBase.Logic.ApplyDamage;
using CodeBase.Logic.ObjectPool;
using CodeBase.Services.LogicFactory;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using UnityEngine;

namespace CodeBase.Player
{
    public class WeaponHitEffect : MonoBehaviour
    {
        private BaseWeaponConfig _config;
        private ShotEffectPool _shotEffectPool;

        public void Construct(BaseWeaponConfig config, ILogicFactory logicFactory)
        {
            _config = config;
            _shotEffectPool = logicFactory.ShotEffectPool;
        }

        public void Play(IApplyDamage damage, RaycastHit raycastHit)
        {
            foreach (ShotEffectSettings effectConfig in _config.ShotEffectSettings.Settings)
                if (effectConfig.ApplyDamageSurfaceId == damage.SurfaceId)
                {
                    FXShotObject fx = _shotEffectPool.Get(effectConfig.ShotEffectId);
                    FXShotObject hole = _shotEffectPool.Get(effectConfig.HoleEffectId);
                    SetFx(fx, damage, raycastHit);
                    SetFx(hole, damage, raycastHit);
                    break;
                }
        }

        private static void SetFx(FXShotObject fx, IApplyDamage applyDamage, RaycastHit raycastHit)
        {
            if (fx == null)
                return;

            applyDamage.OnDestroy += fx.ParentDestroy;
            fx.transform.SetParent(raycastHit.collider.transform);
            fx.transform.SetPositionAndRotation(raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal));
        }
    }
}