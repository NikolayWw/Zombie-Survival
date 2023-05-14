using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Weapon.Firearms
{
    [Serializable]
    public class ShotSpread
    {
        private const float Accuracy = 80f;
        private Camera _mainCamera;

        #region Settings

        [SerializeField] private float accuracyDropPerShot = 15f;
        [SerializeField] private float _lerpRate = 100f;
        [SerializeField] private float _accuracyLimit = 50f;

        #endregion Settings

        public float AccuracySize => Accuracy - _currentAccuracy;
        private float _currentAccuracy;

        private Vector3 _direction;
        public Vector3 Direction => _direction;

        public void Initialize(Camera mainCamera) =>
            _mainCamera = mainCamera;

        public void Update() =>
            _currentAccuracy = Mathf.MoveTowards(_currentAccuracy, Accuracy, _lerpRate * Time.deltaTime);

        public void Spread()
        {
            float accuracyVary = (100 - _currentAccuracy) / 1000;
            _direction = _mainCamera.transform.forward;
            _direction.x += Random.Range(-accuracyVary, accuracyVary);
            _direction.y += Random.Range(-accuracyVary, accuracyVary);
            _direction.z += Random.Range(-accuracyVary, accuracyVary);

            _currentAccuracy -= accuracyDropPerShot;
            AccuracyLimited();
        }

        private void AccuracyLimited()
        {
            if (_currentAccuracy <= -_accuracyLimit)
                _currentAccuracy = -_accuracyLimit;
        }
    }
}