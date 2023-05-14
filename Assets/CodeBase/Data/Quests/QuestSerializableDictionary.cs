using System;

namespace CodeBase.Data.Quests
{
    [Serializable]
    public class QuestSerializableDictionary : SerializableDictionary<QuestId, QuestState>
    { }
}