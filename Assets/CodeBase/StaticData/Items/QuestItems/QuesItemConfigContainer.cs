using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.QuestItems
{
    [CreateAssetMenu(fileName = "New QuestItemConfigContainer", menuName = "Static Data/Items/Quest Items Container", order = 0)]
    public class QuesItemConfigContainer : ScriptableObject
    {
        [field: SerializeField] public List<QuestItemConfig> ItemConfigs { get; private set; }
    }
}