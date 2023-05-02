using CodeBase.UI.Services.Window;
using System;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public WindowId Id { get; private set; }
        public Action<WindowId> OnClosed;

        public void SetId(WindowId id) =>
            Id = id;

        public void Close()
        {
            OnClosed?.Invoke(Id);
            OnClose();
        }

        protected virtual void OnClose() =>
            Destroy(gameObject);
    }
}