using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player.Move;
using UnityEngine;

namespace CodeBase.Player.Move
{
    public class PlayerRotateBody : MonoBehaviour
    {
        [SerializeField] private Transform _bodyRotate;

        private UnityEngine.Camera _mainCamera;
        private MoveStaticData _moveStaticData;

        public void Construct(UnityEngine.Camera mainCamera, IStaticDataService dataService)
        {
            _moveStaticData = dataService.PlayerConfig.MoveStaticData;
            _mainCamera = mainCamera;
        }

        private void LateUpdate() =>
            _bodyRotate.localRotation = Quaternion.Lerp(_bodyRotate.localRotation, _mainCamera.transform.rotation, _moveStaticData.BodyRotateLerpRate * Time.fixedDeltaTime);
    }
}