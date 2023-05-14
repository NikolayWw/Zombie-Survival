using CodeBase.Data.Quests;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;

namespace CodeBase.Npc.Dialogs
{
    public class Lobart : BaseDialog
    {
        private const int CollectTurnip = 6;

        protected override void OnUpdateQuest()
        {
            if (CollectTurnipCondition_Hallo())
            {
                CollectTrip_Start();
            }
            else if (CollectTurnipCondition_Success())
            {
                CollectTrip_Success();
            }
        }

        #region Logic

        protected override bool OnCanSave() =>
            Quests.GetQuest(QuestId.Lobart_CollectTurnip) != QuestState.Running;

        #endregion Logic

        #region Quests

        private void CollectTrip_Start()
        {
            Quests.Change(QuestId.Lobart_CollectTurnip, QuestState.Running);
            QuestPlayer.Play(DialogId.Lobart_CollectTurnipHallo);
            if (SlotsHandler.ContainsItem(QuestItemId.Turnip, CollectTurnip) == false)
                CheckItemPresent(QuestItemId.Turnip, CollectTurnip, QuestPointerId.Lobart_Turnip, QuestPointerId.Lobart);
        }

        private void CollectTrip_Success()
        {
            Quests.Change(QuestId.Lobart_CollectTurnip, QuestState.Success);
            SlotsHandler.DecrementItem(QuestItemId.Turnip, CollectTurnip);
            QuestPlayer.Play(DialogId.Lobart_CollectTurnipSuccess);
            MovePointer(QuestPointerId.Franco);
        }

        #endregion Quests

        #region Conditions

        private bool CollectTurnipCondition_Hallo()
        {
            return Quests.GetQuest(QuestId.Lobart_CollectTurnip) == QuestState.None;
        }

        private bool CollectTurnipCondition_Success()
        {
            return Quests.GetQuest(QuestId.Lobart_CollectTurnip) == QuestState.Running
                   && SlotsHandler.ContainsItem(QuestItemId.Turnip, CollectTurnip);
        }

        #endregion Conditions
    }
}