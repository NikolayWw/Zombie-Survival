using CodeBase.StaticData.NPC;
using UnityEngine;

namespace CodeBase.Logic.Markers
{
    public class QuestTriggerPointMarker : MonoBehaviour
    {
        [field: SerializeField] public float Radius { get; private set; } = 1f;
        [field: SerializeField] public NpcId QuestPointId { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject) == false)
                gameObject.name = $"{QuestPointId}_PointMarker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}