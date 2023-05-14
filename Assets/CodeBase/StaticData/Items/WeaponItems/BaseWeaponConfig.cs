using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    public abstract class BaseWeaponConfig
    {
        [field: SerializeField] public string Name { get; private set; } = string.Empty;
        [field: SerializeField] public WeaponId WeaponId { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject PrefabInPiece { get; private set; }
        [field: SerializeField] public GameObject PrefabInHand { get; private set; }
        [field: SerializeField] public float HitPushForce { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public float AttackDelay { get; private set; } = 0.2f;
        [field: SerializeField] public AudioClip WeaponWhereShow { get; private set; }
        [field: SerializeField] public ShootEffectSettingsContainer ShotEffectSettings { get; private set; }

        public void OnValidate()
        {
            if (Price < 0) Price = 0;
            Validate();
        }

        protected virtual void Validate()
        { }

        protected void ResetWeaponId() =>
            WeaponId = WeaponId.None;
    }
}