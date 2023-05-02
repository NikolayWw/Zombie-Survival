using CodeBase.Data;
using CodeBase.Data.WorldData;
using CodeBase.Logic;
using CodeBase.Logic.Inventory.QuestItemInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.QuestItems
{
    public class QuestItemInteraction : MonoBehaviour
    {
        [SerializeField] private InteractionReporter _reporter;

        private QuestItemId _questItemId;
        private int _amount;

        private PlayerProgress _playerProgress;
        private QuestItemPieceData _pieceData;
        private IUIFactory _uiFactory;
        private HoverAtMessage _hoverAtMessage;
        private QuestSlotsHandler _slotsHandler;

        private bool _isDestroyed;

        public void Construct(IPersistentProgressService persistentProgressService, QuestItemPieceData pieceData, IUIFactory uiFactory, ILogicFactory logicFactory)
        {
            _playerProgress = persistentProgressService.PlayerProgress;
            _pieceData = pieceData;
            _questItemId = pieceData.ItemId;
            _amount = pieceData.Amount;
            _uiFactory = uiFactory;
            _slotsHandler = logicFactory.QuestSlotsHandler;

            _reporter.OnSelect += Select;
            _reporter.OnUse += Use;
            _reporter.OnDeselect += DeSelect;
        }

        private void OnDestroy()
        {
            ClearHoverAtMessage();
        }

        private void Select()
        {
            if (_isDestroyed)
                return;

            if (IsHoverAtMessagePreset() == false)
                _hoverAtMessage = _uiFactory.CreateHoverAtMessage(transform.position, transform, transform);

            _hoverAtMessage.ShowAddItem(_questItemId);
        }

        private void DeSelect()
        {
            if (_isDestroyed)
                return;

            ClearHoverAtMessage();
        }

        private void Use()
        {
            if (_isDestroyed)
                return;

            _slotsHandler.Add(_questItemId, _amount);
            DestroyThis();
        }

        private bool IsHoverAtMessagePreset() =>
            _hoverAtMessage != null && _hoverAtMessage.Destroyed == false;

        private void ClearHoverAtMessage()
        {
            if (IsHoverAtMessagePreset())
            {
                _hoverAtMessage.Close();
                _hoverAtMessage = null;
            }
        }

        private void DestroyThis()
        {
            _playerProgress.WorldData.QuestItemPieceDataList.Remove(_pieceData);
            _isDestroyed = true;
            Destroy(gameObject);
        }
    }
}