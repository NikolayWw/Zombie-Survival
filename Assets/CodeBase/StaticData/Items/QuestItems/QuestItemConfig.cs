using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.QuestItems
{
    [Serializable]
    public class QuestItemConfig
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public QuestItemId QuestItemId { get; private set; }
        [field: SerializeField] public Sprite IconSprite { get; private set; }
        [field: SerializeField] public GameObject PrefabPiece { get; private set; }
        [field: SerializeField] public bool CanStack { get; private set; }
    }
}