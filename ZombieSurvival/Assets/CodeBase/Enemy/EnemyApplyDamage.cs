using CodeBase.Data.WorldData;
using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyApplyDamage : MonoBehaviour, IApplyDamage
    {
        [SerializeField] private EnemyAnimator _animator;

        private EnemyConfig _config;
        private bool _isDead;
        private EnemyPieceData _pieceData;
        public ApplyDamageSurfaceId SurfaceId => _config.ApplyDamageSurfaceId;
        public Action OnDestroy { get; set; }

        public void Construct(EnemyPieceData pieceData, IStaticDataService dataService)
        {
            _config = dataService.ForEnemy(pieceData.EnemyId);
            _pieceData = pieceData;
        }

        public void ApplyDamage(float damage, Vector3 direction)
        {
            if (_isDead)
                return;

            _animator.PlayReactionHit();
            _pieceData.DecrementHealth(damage);
            if (_pieceData.Health <= 0)
            {
                _isDead = true;
                OnDestroy?.Invoke();
            }
        }
    }
}