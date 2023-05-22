using CodeBase.Data.WorldData;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.QuestPointer;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.UI.Windows.QuestPointer
{
    public class QuestPointerWindow : MonoBehaviour
    {
        private Camera _camera;
        private Transform _cameraTransform;
        private IUIFactory _uiFactory;

        private Transform _activeWorldPointer;
        private QuestUIPointerTarget _activeUIPointer;
        private QuestPointerPieceData _pieceData;
        private IStaticDataService _dataService;

        public void Construct(IUIFactory uiFactory, IStaticDataService dataService, IPersistentProgressService persistentProgressService, Camera mainCamera)
        {
            _uiFactory = uiFactory;
            _dataService = dataService;
            _pieceData = persistentProgressService.PlayerProgress.WorldData.QuestPointerPieceData;
            _camera = mainCamera;
            _cameraTransform = mainCamera.transform;
        }

        public void InitWorldPointer(QuestPointerId id)
        {
            _activeWorldPointer = _uiFactory.CreateWorldQuestPointerTarget().transform;
            _activeUIPointer = _uiFactory.CreateUIQuestPointerTarget(transform);
            _activeWorldPointer.position = GetPosition(id);
        }

        public void MovePointer(QuestPointerId id)
        {
            _activeWorldPointer.position = GetPosition(id);
            _pieceData.SetPosition(id);
        }

        private void LateUpdate()
        {
            UpdatePointers();
        }

        private void UpdatePointers()
        {
            // Left, Right, Down, Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            var cameraPoint = _cameraTransform.position + _cameraTransform.forward;

            Vector3 toEnemy = _activeWorldPointer.position - cameraPoint;
            Ray ray = new Ray(cameraPoint, toEnemy);

            float rayMinDistance = Mathf.Infinity;
            int index = 0;

            for (int i = 0; i < 4; i++)
                if (planes[i].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = i;
                    }
                }

            UpdateUIPointer(ray, index, toEnemy, rayMinDistance, _activeUIPointer);
        }

        private void UpdateUIPointer(Ray ray, int index, Vector3 toTarget, float rayMinDistance, QuestUIPointerTarget questUIPointerTarget)
        {
            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toTarget.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);

            bool isOutScreen = toTarget.magnitude > rayMinDistance;

            if (isOutScreen)
                questUIPointerTarget.ShowOutScreen();
            else
                questUIPointerTarget.ShowInScreen();

            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            Quaternion rotation = GetUIPointerRotation(index);
            questUIPointerTarget.SetPositionAndRotate(position, rotation);
        }

        private static Quaternion GetUIPointerRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case 0: return Quaternion.Euler(0f, 0f, 90f);
                case 1: return Quaternion.Euler(0f, 0f, -90f);
                case 2: return Quaternion.Euler(0f, 0f, 180);
                case 3: return Quaternion.Euler(0f, 0f, 0f);
            }
            return Quaternion.identity;
        }

        private Vector3 GetPosition(QuestPointerId id) =>
            _dataService.ForQuestPointerPosition(id).Position;
    }
}