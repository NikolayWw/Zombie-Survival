using CodeBase.Services.Input;
using CodeBase.StaticData.Player.Move;
using UnityEngine;

namespace CodeBase.Player.Move.States
{
    public class GroundMoveState : IMoveState
    {
        private readonly IInputService _inputService;
        private readonly Rigidbody _rigidbody;
        private readonly GroundMoveConfig _config;
        private readonly Transform _mainTransform;
        private readonly SpinningBehindCamera _spinningBehindCamera;
        private readonly ConstantForce _constantForce;
        private readonly Collider[] _groundColliders = new Collider[5];

        private bool _isPause;

        public GroundMoveState(IInputService inputService, Rigidbody rigidbody, GroundMoveConfig config, Transform mainTransform, SpinningBehindCamera spinningBehindCamera, ConstantForce constantForce)
        {
            _inputService = inputService;
            _rigidbody = rigidbody;
            _config = config;
            _mainTransform = mainTransform;
            _spinningBehindCamera = spinningBehindCamera;
            _constantForce = constantForce;
        }

        public void FixedUpdate()
        {
            UpdateMove();
        }

        public void Enter()
        {
            SetRigidbodySettings();
            Subscribe(true);
        }

        public void Exit()
        {
            Subscribe(false);
        }

        public void DrawGizmos()
        {
            var spherePosition = _mainTransform.position + Vector3.down * _config.GroundSphereOffsetY;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spherePosition, _config.GroundSphereRadius);
        }

        #region Pause

        public void Pause()
        { }

        public void Play()
        { }

        public void Freeze()
        {
            _isPause = true;
            _rigidbody.velocity = Vector3.up * _rigidbody.velocity.y;
        }

        public void Unfreeze()
        {
            _isPause = false;
        }

        #endregion Pause

        private void UpdateMove()
        {
            var moveAxis = (_isPause == false) ? _inputService.MoveAxis * _config.Speed : Vector2.zero;
            var moveDirection = _spinningBehindCamera.transform.forward * moveAxis.y + _spinningBehindCamera.transform.right * moveAxis.x;
            _rigidbody.velocity = new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z);
        }

        private void Jump()
        {
            if (Grounded())
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _config.JumpForce, _rigidbody.velocity.z);
        }

        private bool Grounded()
        {
            var spherePosition = _mainTransform.position + Vector3.down * _config.GroundSphereOffsetY;
            int count = Physics.OverlapSphereNonAlloc(spherePosition, _config.GroundSphereRadius, _groundColliders, _config.WhatIsGround);
            return count > 0;
        }

        private void Subscribe(bool isSubscribe)
        {
            if (isSubscribe)
            {
                _inputService.OnJump += Jump;
            }
            else
            {
                _inputService.OnJump -= Jump;
            }
        }

        private void SetRigidbodySettings()
        {
            _rigidbody.useGravity = true;
            _constantForce.force = Vector3.up * _config.ConstantForceY;
        }
    }
}