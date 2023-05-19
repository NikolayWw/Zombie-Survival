using CodeBase.StaticData.QuestPointer;
using UnityEngine;

namespace CodeBase.Logic.Markers
{
    public class QuestPointerPositionWorldMarker : MonoBehaviour
    {
        [field: SerializeField] public QuestPointerId PointerId { get; private set; }

        private void OnValidate()
        {
            if (PrefabChecker.IsPrefab(gameObject) == false)
                gameObject.name = $"{PointerId}_pointerMarker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}