using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public EnemyId EnemyId { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject))
                return;

            gameObject.name = $"{EnemyId}_EnemyMarker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}