using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.GravityGun;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.WeaponItems
{
    [CreateAssetMenu(fileName = "New WeaponConfigContainer", menuName = "Static Data/Items/Weapon Container", order = 0)]
    public class WeaponConfigContainer : ScriptableObject
    {
        [field: SerializeField] public LayerMask ApplyDamageLayer { get; private set; }
        [field: SerializeField] public List<MeleeWeaponConfig> Melee { get; private set; }
        [field: SerializeField] public List<FirearmsConfig> Firearms { get; private set; }
        [field: SerializeField] public List<GravityGunConfig> GravityGuns { get; private set; }

        private void OnValidate()
        {
            Melee.ForEach(x => x.OnValidate());
            Firearms.ForEach(x => x.OnValidate());
            GravityGuns.ForEach(x => x.OnValidate());
        }
    }
}