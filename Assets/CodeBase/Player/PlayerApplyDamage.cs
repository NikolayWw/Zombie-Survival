using CodeBase.Data;
using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerApplyDamage : MonoBehaviour, IApplyDamage
    {
        private PlayerData _playerData;
        public ApplyDamageSurfaceId SurfaceId { get; }
        public Action OnDestroy { get; set; }

        public void Construct(IPersistentProgressService persistentProgressService) =>
            _playerData = persistentProgressService.PlayerProgress.PlayerData;

        public void ApplyDamage(float damage, Vector3 direction) =>
            _playerData.DecrementHealth(damage);
    }
}