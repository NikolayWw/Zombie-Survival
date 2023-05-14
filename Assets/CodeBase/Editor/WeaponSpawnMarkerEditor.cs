using CodeBase.Data.WeaponInventory;
using CodeBase.Logic.Markers.SpawnMarkers;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using CodeBase.StaticData.Items.WeaponItems.GravityGun;
using CodeBase.StaticData.Items.WeaponItems.MeleeWeapon;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(WeaponSpawnMarker))]
    public class WeaponSpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(WeaponSpawnMarker spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(spawner.transform.position, 0.3f);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var marker = (WeaponSpawnMarker)target;
            ShowWeaponSettings(marker);
        }

        private static void ShowWeaponSettings(WeaponSpawnMarker marker)
        {
            if (WeaponDataExtension.IsFirearmsWeapon(marker.WeaponId))
            {
                FirearmWeaponData data = marker.WeaponDataContainer.GetData() as FirearmWeaponData ?? (new WeaponDataContainer(new FirearmWeaponData(marker.WeaponId, 0, 0)).GetData() as FirearmWeaponData);
                int magazine = EditorGUILayout.IntField("Magazine", data.Magazine);
                int ammo = EditorGUILayout.IntField("Ammo", data.Amount);
                var newData = new WeaponDataContainer(new FirearmWeaponData(marker.WeaponId, magazine, ammo));
                marker.SetWeaponData(newData);
            }
            else if (WeaponDataExtension.IsMeleeWeapon(marker.WeaponId))
            {
                marker.SetWeaponData(new WeaponDataContainer(new MeleeWeaponData(marker.WeaponId)));
            }
            else if (WeaponDataExtension.IsGravityGun(marker.WeaponId))
            {
                marker.SetWeaponData(new WeaponDataContainer(new GravityGunData(marker.WeaponId)));
            }
            else
            {
                Debug.LogError("wrong data");
            }

            EditorUtility.SetDirty(marker);
        }
    }
}