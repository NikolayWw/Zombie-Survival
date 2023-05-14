using UnityEngine;

namespace CodeBase.Npc
{
    public class QuestPlayerPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform PointTransform { get; private set; }
        [field: SerializeField] public Vector3 Size { get; private set; } = Vector3.one;

        private void OnDrawGizmos()
        {
            if (PointTransform != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(PointTransform.position, Size);
            }
        }
    }
}