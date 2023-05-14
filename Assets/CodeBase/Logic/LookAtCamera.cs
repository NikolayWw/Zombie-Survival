using UnityEngine;

namespace CodeBase.Logic
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _playerCamera;

        public void Construct(Camera playerCamera)
        {
            _playerCamera = playerCamera;
        }

        private void LateUpdate()
        {
            transform.forward = -_playerCamera.transform.forward;
        }
    }
}