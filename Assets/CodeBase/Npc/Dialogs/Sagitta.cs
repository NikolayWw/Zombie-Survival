using CodeBase.Data.Quests;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;

namespace CodeBase.Npc.Dialogs
{
    public class Sagitta : BaseDialog
    {
        protected override void OnUpdateQuest()
        {
            if (TheklaPaketCondition_Hallo())
            {
                TheklaPaket_Hallo();
            }
        }

        #region Quests

        private void TheklaPaket_Hallo()
        {
            QuestPlayer.Play(DialogId.Thekla_SagittaPaketHallo);
            SlotsHandler.Add(QuestItemId.Paket, 1);
            MovePointer(QuestPointerId.Thekla);
        }

        #endregion Quests

        #region Conditions

        private bool TheklaPaketCondition_Hallo()
        {
            return Quests.GetQuest(QuestId.Thekla_Paket) == QuestState.Running
                && SlotsHandler.ContainsItem(QuestItemId.Paket, 1) == false;
        }

        #endregion Conditions
    }
}