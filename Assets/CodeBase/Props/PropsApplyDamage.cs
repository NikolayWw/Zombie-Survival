using CodeBase.Data.Props;
using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Props;
using System;
using UnityEngine;

namespace CodeBase.Props
{
    public class PropsApplyDamage : MonoBehaviour, IApplyDamage
    {
        [SerializeField] private Rigidbody _rigidbody;
        private PropsConfig _config;
        private PropsPieceData _pieceData;
        private IGameFactory _gameFactory;
        public ApplyDamageSurfaceId SurfaceId => _config.ApplyDamageSurfaceId;
        public Action OnDestroy { get; set; }

        public void Construct(PropsPieceData pieceData, IGameFactory gameFactory, IStaticDataService dataService)
        {
            _pieceData = pieceData;
            _gameFactory = gameFactory;
            _config = dataService.ForProps(pieceData.PropsId);
        }

        public void ApplyDamage(float damage, Vector3 directionAndForce)
        {
            _rigidbody.AddForce(directionAndForce, ForceMode.VelocityChange);
            _pieceData.Decrement(damage);
            if (_pieceData.CurrentHealth <= 0)
            {
                OnDestroy?.Invoke();
                _gameFactory.CreatePropsFX(_pieceData.PropsId, transform.position);
                Destroy(gameObject);
            }
        }
    }
}