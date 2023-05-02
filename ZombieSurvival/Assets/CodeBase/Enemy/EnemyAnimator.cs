using CodeBase.Infrastructure.Logic;
using CodeBase.Logic.Pause;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, ICoroutineRunner, IPause
    {
        private readonly int ReactionHitHash = Animator.StringToHash("ReactionHit");
        private readonly int DeadHash = Animator.StringToHash("Dead");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int MoveHash = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        public Action<EnemyAnimationEventReporterId> OnEvent;
        private EnemyAnimationTimer _timer;
        public bool IsAttackPlay { get; private set; }

        private float _pauseAnimatorSpeed;

        private void Start()
        {
            _timer = new EnemyAnimationTimer(this, 0.1f, () => _animator.SetBool(ReactionHitHash, false));
        }

        private void Update()
        {
            UpdateMove();
        }

        public void PlayReactionHit()
        {
            _animator.SetBool(ReactionHitHash, true);
            _timer.Start();
        }

        public void PlayDead() =>
            _animator.SetTrigger(DeadHash);

        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        #region Pause

        public void Play()
        {
            _animator.speed = _pauseAnimatorSpeed;
        }

        public void Pause()
        {
            _pauseAnimatorSpeed = _animator.speed;
            _animator.speed = 0;
        }

        #endregion Pause

        private void AnimationEventReader(EnemyAnimationEventReporterId id)//use animator event
        {
            switch (id)
            {
                case EnemyAnimationEventReporterId.ReactionHit_Start:
                    _animator.SetBool(ReactionHitHash, false);
                    break;

                case EnemyAnimationEventReporterId.Attack_Start:
                    IsAttackPlay = true;
                    break;

                case EnemyAnimationEventReporterId.Attack_End:
                    IsAttackPlay = false;
                    break;
            }
            OnEvent?.Invoke(id);
        }

        private void UpdateMove()
        {
            const float dampTime = 1.5f;
            _animator.SetFloat(MoveHash, _agent.velocity.magnitude, dampTime, Time.deltaTime);
        }
    }
}