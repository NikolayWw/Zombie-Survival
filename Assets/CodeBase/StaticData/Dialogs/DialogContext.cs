using System;
using UnityEngine;

namespace CodeBase.StaticData.Dialogs
{
    [Serializable]
    public class DialogContext
    {
        [field: SerializeField] public string Context { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}