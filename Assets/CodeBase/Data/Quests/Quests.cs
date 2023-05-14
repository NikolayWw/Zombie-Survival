using System;
using UnityEngine;

namespace CodeBase.Data.Quests
{
    [Serializable]
    public class Quests
    {
        [SerializeField] private QuestSerializableDictionary _questSerializableDictionary = new QuestSerializableDictionary();

        public void Change(QuestId id, QuestState state) =>
            _questSerializableDictionary.Dictionary[id] = state;

        public QuestState GetQuest(QuestId id) =>
            _questSerializableDictionary.Dictionary.TryGetValue(id, out var state) ? state : QuestState.None;
    }
}