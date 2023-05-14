using CodeBase.Services.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Input
{
    public abstract class BaseInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        protected IInputService InputService { get; private set; }
        protected Action OnDown;
        protected Action OnUp;

        public void Construct(IInputService inputService) =>
            InputService = inputService;

        public void OnPointerDown(PointerEventData eventData) =>
            OnDown?.Invoke();

        public void OnPointerUp(PointerEventData eventData) =>
            OnUp?.Invoke();
    }
}