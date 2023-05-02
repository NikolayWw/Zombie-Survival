using CodeBase.BehaviourTree.Tree;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.BehaviourTree.Tasks
{
    public class CheckAttackDistance_Task : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly float _attackDistance;

        public CheckAttackDistance_Task(NavMeshAgent agent, float attackDistance)
        {
            _agent = agent;
            _attackDistance = attackDistance;
        }

        public override bool Evaluated()
        {
            Transform target = GetTargetTransform();
            if (target == null)
                return false;

            return Vector3.Distance(_agent.transform.position, target.position) <= _attackDistance;
        }
    }
}