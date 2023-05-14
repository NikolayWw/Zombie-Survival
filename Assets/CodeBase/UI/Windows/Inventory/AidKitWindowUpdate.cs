using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory
{
    public class AidKitWindowUpdate : MonoBehaviour
    {
        [SerializeField] private TMP_Text _firstAidKitText;
        private PlayerData _playerData;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _playerData = persistentProgressService.PlayerProgress.PlayerData;
            _playerData.OnFirstAidKitChange += Refresh;
        }

        private void Start() =>
            Refresh();

        private void OnDestroy() =>
            _playerData.OnFirstAidKitChange -= Refresh;

        private void Refresh() =>
            _firstAidKitText.text = _playerData.AidKit.ToString();
    }
}