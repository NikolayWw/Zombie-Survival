using System.Collections;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Elements
{
    public class CameraLookButton : MonoBehaviour, IDragHandler, IPointerUpHandler
    {
        private bool _dragging;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            CheckPlatform();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragging)
                return;

            _dragging = true;
            StartCoroutine(UpdateTouch(eventData));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputService.UpdateCameraAxis(Vector2.zero);
        }

        private IEnumerator UpdateTouch(PointerEventData eventData)
        {
            while (eventData.dragging)
            {
                _inputService.UpdateCameraAxis(eventData.delta);
                yield return null;
            }
            _inputService.UpdateCameraAxis(Vector2.zero);
            _dragging = false;
        }

        private void CheckPlatform()
        {
            bool platformSupported = false;
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    platformSupported = true;
                    break;
            }

            enabled = platformSupported;
        }
    }
}