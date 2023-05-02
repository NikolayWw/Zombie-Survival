using System;
using UnityEngine;

namespace CodeBase.StaticData.Dialogs
{
    [Serializable]
    public class DialogContextData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public DialogId DialogId { get; private set; }
        [field: SerializeField] public DialogContext Context { get; private set; }

        public void OnValidate()
        {
            _inspectorName = DialogId.ToString();
        }
    }
}