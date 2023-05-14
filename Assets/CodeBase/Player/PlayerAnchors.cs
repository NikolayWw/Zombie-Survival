using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnchors : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraAnchor { get; private set; }
        [field: SerializeField] public Transform ReloadMagazineAnchor { get; private set; }
    }
}