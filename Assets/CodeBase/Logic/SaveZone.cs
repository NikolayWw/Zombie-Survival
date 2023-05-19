using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveZone : MonoBehaviour
    {
        [SerializeField] private InteractionReporter _interactionReporter;
        [SerializeField] private Canvas _messageCanvas;
        [SerializeField] private Transform _rotateMessage;

        private ISaveLoadService _loadService;
        private Camera _camera;

        public void Construct(ISaveLoadService loadService, Camera mainCamera)
        {
            _loadService = loadService;
            _messageCanvas.worldCamera = mainCamera;
            _camera = mainCamera;
            _interactionReporter.OnUse += _loadService.SaveProgress;
        }

        private void LateUpdate()
        {
            UpdateMessageRotate();
        }

        private void UpdateMessageRotate() =>
            _rotateMessage.forward = _camera.transform.forward;
    }
}