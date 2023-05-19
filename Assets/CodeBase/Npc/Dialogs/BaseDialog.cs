using CodeBase.Data.Quests;
using CodeBase.Logic.Inventory.QuestItemInventory;
using CodeBase.Logic.Quest;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows.QuestPointer;
using UnityEngine;

namespace CodeBase.Npc.Dialogs
{
    public abstract class BaseDialog : MonoBehaviour, ISaveLoad
    {
        [SerializeField] private NpcFindPlayerQuestReporter _findPlayerQuest;
        protected Quests Quests { get; private set; }
        protected QuestPlayer QuestPlayer { get; private set; }
        protected QuestSlotsHandler SlotsHandler { get; private set; }
        public bool CanSave => OnCanSave();
        private MovePointerConditionContainer _movePointerConditionContainer;
        private QuestPointerWindow _questPointerWindow;

        public void BaseConstruct(IPersistentProgressService persistentProgressService, IUIFactory uiFactory, ILogicFactory logicFactory)
        {
            Quests = persistentProgressService.PlayerProgress.Quests;
            SlotsHandler = logicFactory.QuestSlotsHandler;
            QuestPlayer = logicFactory.QuestPlayer;
            _questPointerWindow = uiFactory.QuestPointerWindow;

            _findPlayerQuest.OnPlayerTriggered += UpdateQuest;
        }

        #region Save/Load

        public void Save() =>
            OnSave();

        public void Load() =>
            OnLoad();

        protected virtual bool OnCanSave() =>
            true;

        protected virtual void OnSave()
        { }

        protected virtual void OnLoad()
        { }

        #endregion Save/Load

        protected void MovePointer(QuestPointerId id)
        {
            _questPointerWindow.MovePointer(id);
        }

        protected void CheckItemPresent(QuestItemId id, int amount, QuestPointerId move, QuestPointerId moveItemPresent)
        {
            _movePointerConditionContainer = new MovePointerConditionContainer(id, amount, moveItemPresent);
            MovePointer(move);
            SlotsHandler.OnSlotsDataChange += CheckItemPresent;
        }

        protected abstract void OnUpdateQuest();

        private void UpdateQuest()
        {
            if (QuestPlayer.IsPlayed)
                return;

            OnUpdateQuest();
        }

        private void CheckItemPresent()
        {
            if (SlotsHandler.ContainsItem(_movePointerConditionContainer.QuestItemId, _movePointerConditionContainer.Amount))
            {
                SlotsHandler.OnSlotsDataChange -= CheckItemPresent;
                _questPointerWindow.MovePointer(_movePointerConditionContainer.PointerId);
            }
        }
    }
}