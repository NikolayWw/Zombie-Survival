using UnityEngine;

namespace CodeBase.Player.Move
{
    public class SpinningBehindCamera : MonoBehaviour
    {
        private UnityEngine.Camera _mainCamera;

        public void Construct(UnityEngine.Camera mainCamera) =>
            _mainCamera = mainCamera;

        private void LateUpdate() =>
            transform.eulerAngles = new Vector3(0, _mainCamera.transform.eulerAngles.y, 0);
    }
}