using CodeBase.Services.Input;
using CodeBase.StaticData.Player.Move;
using UnityEngine;

namespace CodeBase.Player.Move.States
{
    public class StairMoveState : IMoveState
    {
        private readonly IInputService _inputService;
        private readonly Rigidbody _rigidbody;
        private readonly StairMoveConfig _config;
        private readonly UnityEngine.Camera _mainCamera;
        private readonly ConstantForce _constantForce;

        public StairMoveState(IInputService inputService, Rigidbody rigidbody, StairMoveConfig config, UnityEngine.Camera mainCamera, ConstantForce constantForce)
        {
            _inputService = inputService;
            _rigidbody = rigidbody;
            _config = config;
            _mainCamera = mainCamera;
            _constantForce = constantForce;
        }

        public void Enter()
        {
            SetRigidbodySettings();
        }

        public void FixedUpdate()
        {
            UpdateMove();
        }

        public void Exit()
        { }

        #region Pause

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void Freeze()
        {
        }

        public void Unfreeze()
        {
        }

        #endregion Pause

        public void DrawGizmos()
        { }

        private void UpdateMove()
        {
            var moveAxis = _inputService.MoveAxis * _config.Speed;
            var moveDirection = _mainCamera.transform.forward * moveAxis.y + _mainCamera.transform.right * moveAxis.x;
            _rigidbody.velocity = moveDirection;
        }

        private void SetRigidbodySettings()
        {
            _rigidbody.useGravity = false;
            _constantForce.force = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}