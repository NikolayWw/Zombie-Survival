using CodeBase.Services.Input;
using System.Collections;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Input
{
    public class CameraLookButton : MonoBehaviour, IDragHandler
    {
        private bool _enable;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_enable)
                return;

            _enable = true;
            StartCoroutine(UpdateTouch(eventData));
        }

        private IEnumerator UpdateTouch(PointerEventData eventData)
        {
            while (eventData.dragging)
            {
                _inputService.UpdateCameraAxis(eventData.delta);
                yield return null;
            }
            _inputService.UpdateCameraAxis(Vector2.zero);
            _enable = false;
        }
    }
}