using CodeBase.BehaviourTree.Tasks;
using CodeBase.BehaviourTree.Tree;
using CodeBase.Data.WorldData;
using CodeBase.Enemy;
using CodeBase.Logic.Pause;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.BehaviourTree.Behaviour
{
    public class EnemyBehaviour : BaseBehaviourTree, ISaveLoad, IPause
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyApplyDamage _applyDamage;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private CapsuleCollider _collider;
        public bool CanSave => true;

        private EnemyPieceData _pieceData;
        private EnemyConfig _config;

        public void Construct(EnemyPieceData pieceData, IStaticDataService dataService)
        {
            _pieceData = pieceData;
            _config = dataService.ForEnemy(_pieceData.EnemyId);

            _applyDamage.OnDestroy += Dead;
        }

        public override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequencer(new List<Node>
                {
                    new CheckAttackDistance_Task(_agent, _config.AttackDistance),
                    new Attack_Task(_config.AttackDelay, _config.Damage, _enemyAnimator)
                }),
                new Sequencer(new List<Node>
                {
                    new FindTarget_Task(transform, _config.WhatIsTargetLayer, _config.FindTargetRadius),
                    new FollowTarget_Task(_agent, _config.MoveToTargetSpeed, _enemyAnimator),
                }),
                new EnemyIdle_Task()
            });
            return root;
        }

        #region Pause

        public void Pause()
        {
            _agent.enabled = false;
            enabled = false;
        }

        public void Play()
        {
            _agent.enabled = true;
            enabled = true;
        }

        #endregion Pause

        #region Save

        public void Save()
        {
            _pieceData.SetPosition(_agent.transform.position);
        }

        public void Load()
        { }

        #endregion Save

        private void Dead()
        {
            enabled = false;
            _enemyAnimator.PlayDead();
            _agent.SetDestination(transform.position);

            Vector3 position = new Vector3(-0.39f, -1f, 1.03f);
            Quaternion rotation = Quaternion.Euler(90f, 0, 200f);
            Vector3 capsuleOffset = new Vector3(0, 0, -1);

            _collider.transform.localPosition = position;
            _collider.transform.localRotation = rotation;
            _collider.center += capsuleOffset;

            _agent.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (_config != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, _config.FindTargetRadius);
            }
        }
    }
}