using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class InteractionReporter : MonoBehaviour
    {
        public Action OnSelect;
        public Action OnUse;
        public Action OnDeselect;

        public void SelectSend() =>
            OnSelect?.Invoke();

        public void UseSend() =>
            OnUse?.Invoke();

        public void DeselectSend() =>
            OnDeselect?.Invoke();
    }
}