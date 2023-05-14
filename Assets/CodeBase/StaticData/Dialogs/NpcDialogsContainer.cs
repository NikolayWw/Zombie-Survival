using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Dialogs
{
    [CreateAssetMenu(fileName = "New NpcDialogsContainer", menuName = "Static Data/Npc Dialogs Container", order = 0)]
    public class NpcDialogsContainer : ScriptableObject
    {
        [field: SerializeField] public List<DialogContextData> Dialogs { get; private set; }

        private void OnValidate()
        {
            Dialogs.ForEach(x => x.OnValidate());
        }
    }
}