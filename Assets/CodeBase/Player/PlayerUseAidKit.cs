using CodeBase.Data;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerUseAidKit : MonoBehaviour
    {
        private PlayerData _playerData;
        private IStaticDataService _dataService;

        public void Construct(IInputService inputService, IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _dataService = dataService;

            inputService.OnUseAidKit += Heal;
        }

        private void Heal()
        {
            if (_playerData.AidKit > 0 && _playerData.Health < _dataService.PlayerConfig.MaxHealth)
            {
                _playerData.DecrementAidKit(1);
                _playerData.IncrementHealth(_dataService.AidKitConfig.HealCount);
            }
        }
    }
}