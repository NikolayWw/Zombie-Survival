using CodeBase.Data.Quests;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;

namespace CodeBase.Npc.Dialogs
{
    public class Franco : BaseDialog
    {
        protected override void OnUpdateQuest()
        {
            if (FindStoneTabletCondition_Hallo())
            {
                FindStoneTablet_Start();
            }
            else if (FindStoneTabletCondition_Success())
            {
                FindStoneTablet_Success();
            }
        }

        #region Logic

        protected override bool OnCanSave() =>
            Quests.GetQuest(QuestId.Franco_FindStoneTablet) != QuestState.Running;

        #endregion Logic

        #region Quests

        private void FindStoneTablet_Start()
        {
            Quests.Change(QuestId.Franco_FindStoneTablet, QuestState.Running);
            QuestPlayer.Play(DialogId.Franco_FindStoneTabletHallo);

            if (SlotsHandler.ContainsItem(QuestItemId.StoneTablet, 1) == false)
                CheckItemPresent(QuestItemId.StoneTablet, 1, QuestPointerId.Franco_StoneTablet, QuestPointerId.Franco);
        }

        private void FindStoneTablet_Success()
        {
            Quests.Change(QuestId.Franco_FindStoneTablet, QuestState.Success);
            SlotsHandler.DecrementItem(QuestItemId.StoneTablet, 1);
            QuestPlayer.Play(DialogId.Franco_FindStoneTabletSuccess);
            MovePointer(QuestPointerId.Thekla);
        }

        #endregion Quests

        #region Conditions

        private bool FindStoneTabletCondition_Hallo()
        {
            return Quests.GetQuest(QuestId.Franco_FindStoneTablet) == QuestState.None
                && Quests.GetQuest(QuestId.Lobart_CollectTurnip) == QuestState.Success;
        }

        private bool FindStoneTabletCondition_Success()
        {
            return Quests.GetQuest(QuestId.Franco_FindStoneTablet) == QuestState.Running
                   && SlotsHandler.ContainsItem(QuestItemId.StoneTablet, 1);
        }

        #endregion Conditions
    }
}