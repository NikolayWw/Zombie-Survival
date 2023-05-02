using CodeBase.StaticData.Props;
using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class PropsSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public PropsId PropsId { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject) == false)
                gameObject.name = $"{PropsId}_SpawnMarker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}