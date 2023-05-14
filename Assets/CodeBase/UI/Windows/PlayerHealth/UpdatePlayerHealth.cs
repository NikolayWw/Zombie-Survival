using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.PlayerHealth
{
    public class UpdatePlayerHealth : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        private PlayerData _playerData;
        private PlayerConfig _playerConfig;

        public void Construct(IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _playerConfig = dataService.PlayerConfig;

            _playerData.OnHealthChange += RefreshHealth;
        }

        private void Start() =>
            RefreshHealth();

        private void OnDestroy() =>
            _playerData.OnHealthChange -= RefreshHealth;

        private void RefreshHealth() =>
            _healthImage.fillAmount = _playerData.Health / _playerConfig.MaxHealth;
    }
}