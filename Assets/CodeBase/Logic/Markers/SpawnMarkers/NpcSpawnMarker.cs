using CodeBase.StaticData.NPC;
using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class NpcSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public NpcId NpcId { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject) == false)
                gameObject.name = $"{NpcId}_SpawnMarker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}