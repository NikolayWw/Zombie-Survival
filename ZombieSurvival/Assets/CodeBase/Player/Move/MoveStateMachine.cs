using CodeBase.Data;
using CodeBase.Logic.Pause;
using CodeBase.Player.Move.States;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player.Move
{
    public class MoveStateMachine : MonoBehaviour, IFreeze, ISaveLoad
    {
        [SerializeField] private SpinningBehindCamera _spinningBehindCamera;
        [SerializeField] private ConstantForce _constantForce;
        [SerializeField] private Rigidbody _rigidbody;
        public bool CanSave => true;

        private Dictionary<MoveStateId, IMoveState> _states;
        private IMoveState _activeState;
        private PlayerData _playerData;

        private Vector3 _pauseRigidbodyVelocity;

        public void Construct(IInputService inputService, UnityEngine.Camera mainCamera, IStaticDataService dataService, IPersistentProgressService persistentProgressService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            InitStates(inputService, dataService, mainCamera);
        }

        private void FixedUpdate()
        {
            _activeState.FixedUpdate();
        }

        public void Enter(MoveStateId id)
        {
            _activeState?.Exit();
            _activeState = _states[id];
            _activeState.Enter();
        }

        #region Save/Load

        public void Save()
        {
            _playerData.SetPosition(transform.position);
        }

        public void Load()
        { }

        #endregion Save/Load

        #region Pause

        public void Pause()
        {
            _pauseRigidbodyVelocity = _rigidbody.velocity;
            _rigidbody.isKinematic = true;
        }

        public void Play()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _pauseRigidbodyVelocity;
        }

        public void Freeze()
        {
            _activeState.Freeze();
        }

        public void Unfreeze()
        {
            _activeState.Unfreeze();
        }

        #endregion Pause

        private void InitStates(IInputService inputService, IStaticDataService dataService, UnityEngine.Camera mainCamera)
        {
            _states = new Dictionary<MoveStateId, IMoveState>
            {
                [MoveStateId.MoveGround] = new GroundMoveState(inputService, _rigidbody, dataService.PlayerConfig.MoveStaticData.GroundMoveConfig, transform, _spinningBehindCamera, _constantForce),
                [MoveStateId.Stair] = new StairMoveState(inputService, _rigidbody, dataService.PlayerConfig.MoveStaticData.StairMoveConfig, mainCamera, _constantForce),
            };
        }

        private void OnDrawGizmos()
        {
            _activeState?.DrawGizmos();
        }
    }
}