using CodeBase.Data.Quests;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.QuestPointer;

namespace CodeBase.Npc.Dialogs
{
    public class Thekla : BaseDialog
    {
        protected override void OnUpdateQuest()
        {
            if (PaketCondition_Hallo())
            {
                Paket_Hallo();
            }
            else if (PaketCondition_Success())
            {
                Paket_Success();
            }
        }

        #region Quests

        private void Paket_Hallo()
        {
            Quests.Change(QuestId.Thekla_Paket, QuestState.Running);
            QuestPlayer.Play(DialogId.Thekla_PaketHallo, DialogId.Thekla_PaketGoSagitta);
            MovePointer(QuestPointerId.Sagitta);
        }

        private void Paket_Success()
        {
            Quests.Change(QuestId.Thekla_Paket, QuestState.Success);
            SlotsHandler.DecrementItem(QuestItemId.Paket, 1);
            QuestPlayer.Play(DialogId.Thekla_SagittaPaketSuccess);
        }

        #endregion Quests

        #region Conditions

        private bool PaketCondition_Hallo()
        {
            return Quests.GetQuest(QuestId.Thekla_Paket) == QuestState.None
                && Quests.GetQuest(QuestId.Franco_FindStoneTablet) == QuestState.Success;
        }

        private bool PaketCondition_Success()
        {
            return Quests.GetQuest(QuestId.Thekla_Paket) == QuestState.Running
                   && SlotsHandler.ContainsItem(QuestItemId.Paket, 1);
        }

        #endregion Conditions
    }
}