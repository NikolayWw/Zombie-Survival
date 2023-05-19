using UnityEngine;

namespace CodeBase.Player.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;

        public void Construct(Transform target) =>
            _target = target;

        private void LateUpdate() =>
            transform.position = _target.position;
    }
}