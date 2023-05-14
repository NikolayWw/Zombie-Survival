using UnityEngine;

namespace CodeBase.Logic.Markers.SpawnMarkers
{
    public class SaveZoneSpawnMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}