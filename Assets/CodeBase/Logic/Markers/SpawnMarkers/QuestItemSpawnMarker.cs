using CodeBase.StaticData.Items.QuestItems;
using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class QuestItemSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public QuestItemId Id { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject))
                return;

            gameObject.name = $"{Id}_QuestMarker";
            if (Amount < 1) Amount = 1;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}