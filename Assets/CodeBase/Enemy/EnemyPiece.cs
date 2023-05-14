using CodeBase.Data.WorldData;
using CodeBase.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyPiece : MonoBehaviour
    {
        [SerializeField] private EnemyApplyDamage _applyDamage;
        private List<EnemyPieceData> _pieceDataList;
        private EnemyPieceData _pieceData;

        public void Construct(IPersistentProgressService progressService, EnemyPieceData pieceData)
        {
            _pieceDataList = progressService.PlayerProgress.WorldData.EnemyDataPieceList;
            _pieceData = pieceData;

            _applyDamage.OnDestroy += RemovePieceData;
        }

        private void RemovePieceData()
        {
            if (_pieceDataList.Contains(_pieceData))
                _pieceDataList.Remove(_pieceData);
        }
    }
}