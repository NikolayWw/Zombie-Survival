using CodeBase.StaticData.Items.WeaponItems;
using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class WeaponSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public WeaponId WeaponId { get; private set; }

        [HideInInspector] [SerializeField] private WeaponDataContainer _weaponDataContainer;

        public WeaponDataContainer WeaponDataContainer => _weaponDataContainer;

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject) == false)
                gameObject.name = WeaponId + "_" + "WeaponMarker";
        }

        public void SetWeaponData(WeaponDataContainer weaponDataContainer)
        {
            _weaponDataContainer = weaponDataContainer;
        }
    }
}