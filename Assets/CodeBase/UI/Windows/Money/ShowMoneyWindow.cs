using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Money
{
    public class ShowMoneyWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _moneyText;
        private PlayerData _playerData;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _playerData.OnChangeMoney += RefreshMoney;
        }

        private void Start()
        {
            RefreshMoney();
        }

        private void OnDestroy()
        {
            _playerData.OnChangeMoney -= RefreshMoney;
        }

        private void RefreshMoney()
        {
            _moneyText.text = $"Money: {_playerData.Money}";
        }
    }
}