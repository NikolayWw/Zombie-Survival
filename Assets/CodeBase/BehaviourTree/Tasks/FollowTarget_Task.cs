using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.BehaviourTree.Tasks
{
    public class FollowTarget_Task : Node
    {
        private readonly float _moveToTargetSpeed;
        private readonly EnemyAnimator _animator;
        private readonly NavMeshAgent _agent;

        public FollowTarget_Task(NavMeshAgent agent, float moveToTargetSpeed, EnemyAnimator animator)
        {
            _agent = agent;
            _moveToTargetSpeed = moveToTargetSpeed;
            _animator = animator;
        }

        public override bool Evaluated()
        {
            Transform target = GetTargetTransform();
            if (target == null)
                return false;

            if (_animator.IsAttackPlay == false)
            {
                _agent.speed = _moveToTargetSpeed;
                _agent.SetDestination(target.position);
            }
            return true;
        }
    }
}