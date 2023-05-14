using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player.Move;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player.Move
{
    public class SwitchMoveState : MonoBehaviour
    {
        [SerializeField] private MoveStateMachine _moveStateMachine;
        private IEnumerator _checkStateEnumerator;
        private SwitchMoveConfig _config;

        private MoveStateId _currentState;

        private readonly Collider[] _colliders = new Collider[5];

        public void Construct(IStaticDataService dataService)
        {
            _config = dataService.PlayerConfig.MoveStaticData.SwitchMoveConfig;
            _checkStateEnumerator = CheckState();
        }

        private void Start()
        {
            _moveStateMachine.Enter(MoveStateId.MoveGround);
            StartCoroutine(_checkStateEnumerator);
        }

        private IEnumerator CheckState()
        {
            var wait = new WaitForSeconds(_config.DelayCheck);
            while (true)
            {
                ChangeState();
                yield return wait;
            }
        }

        private void ChangeState()
        {
            var spherePosition = transform.position + Vector3.up * _config.SphereOffsetY;
            int count = Physics.OverlapSphereNonAlloc(spherePosition, _config.SphereRadius, _colliders, _config.CheckLayers);

            MoveStateId newState = MoveStateId.MoveGround;
            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out MoveState state))
                {
                    newState = state.Id;
                    break;
                }
            }
            if (_currentState != newState)
            {
                _currentState = newState;
                _moveStateMachine.Enter(newState);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_config != null)
            {
                Gizmos.color = Color.green;
                var spherePosition = transform.position + Vector3.up * _config.SphereOffsetY;
                Gizmos.DrawWireSphere(spherePosition, _config.SphereRadius);
            }
        }
    }
}