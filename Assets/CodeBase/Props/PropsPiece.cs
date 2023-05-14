using CodeBase.Data.Props;
using CodeBase.Logic.Pause;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Props
{
    public class PropsPiece : MonoBehaviour, ISaveLoad, IPause
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PropsApplyDamage _applyDamage;
        public bool CanSave => true;

        private PropsPieceData _pieceData;
        private List<PropsPieceData> _pieceDataList;
        private IGameFactory _gameFactory;

        private Vector3 _pauseRigidbodyVelocity;
        private Vector3 _pauseRigidbodyAngularVelocity;

        public void Construct(PropsPieceData pieceData, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _pieceData = pieceData;
            _gameFactory = gameFactory;
            _pieceDataList = persistentProgressService.PlayerProgress.WorldData.EnvironmentDataList;
            _applyDamage.OnDestroy += RemoveData;
        }

        #region Pause

        public void Pause()
        {
            _pauseRigidbodyVelocity = _rigidbody.velocity;
            _pauseRigidbodyAngularVelocity = _rigidbody.angularVelocity;
            _rigidbody.isKinematic = true;
        }

        public void Play()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _pauseRigidbodyVelocity;
            _rigidbody.angularVelocity = _pauseRigidbodyAngularVelocity;
        }

        #endregion Pause

        #region Save/Load

        public void Save()
        {
            _pieceData.SetPosition(transform.position);
        }

        public void Load()
        { }

        #endregion Save/Load

        private void RemoveData()
        {
            _gameFactory.Pauses.Remove(this);
            _gameFactory.SaveLoads.Remove(this);

            if (_pieceDataList.Contains(_pieceData))
                _pieceDataList.Remove(_pieceData);
        }
    }
}