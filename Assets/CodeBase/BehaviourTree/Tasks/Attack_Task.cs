using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using CodeBase.Logic.ApplyDamage;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class Attack_Task : Node
    {
        private readonly EnemyAnimator _animator;
        private readonly float _attackDelay;
        private readonly float _damage;

        private float _currentDelay;
        private IApplyDamage _target;

        public Attack_Task(float attackDelay, float damage, EnemyAnimator animator)
        {
            _animator = animator;
            _attackDelay = attackDelay;
            _damage = damage;
            _currentDelay = attackDelay;
            _animator.OnEvent += Attack;
        }

        public override bool Evaluated()
        {
            _target = GetTarget();
            if (_target == null)
            {
                return false;
            }
            _currentDelay += Time.deltaTime;
            if (_currentDelay >= _attackDelay && _animator.IsAttackPlay == false)
            {
                _currentDelay = 0.0f;
                _animator.PlayAttack();
            }

            return true;
        }

        private void Attack(EnemyAnimationEventReporterId id)
        {
            if (EnemyAnimationEventReporterId.Attack == id)
                _target?.ApplyDamage(_damage, Vector3.zero);
        }
    }
}