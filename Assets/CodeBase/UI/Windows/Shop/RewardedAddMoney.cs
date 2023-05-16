using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class RewardedAddMoney : MonoBehaviour
    {
        [SerializeField] private Button _showAdsButton;
        [SerializeField] private GameObject[] _adActiveObjects = Array.Empty<GameObject>();
        [SerializeField] private GameObject[] _adInActiveObjects = Array.Empty<GameObject>();

        private IAdsService _adsService;
        private IPersistentProgressService _persistentProgressService;

        public void Construct(IAdsService service, IPersistentProgressService persistentProgressService)
        {
            _adsService = service;
            _persistentProgressService = persistentProgressService;
            _adsService.RewardedVideoReady += RefreshAvailableAd;
        }

        public void Initialize()
        {
            _showAdsButton.onClick.AddListener(OnShowAdClicked);
            RefreshAvailableAd();
        }

        public void Clean() =>
            _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() =>
            _adsService.ShowRewarded(OnVideoFinished);

        private void OnVideoFinished() =>
            _persistentProgressService.PlayerProgress.PlayerData.IncrementMoney(100);

        private void RefreshAvailableAd()
        {
            bool ready = _adsService.AdsReady;

            foreach (var activeObject in _adActiveObjects) activeObject.SetActive(ready);
            ready = !ready;
            foreach (var inActiveObject in _adInActiveObjects) inActiveObject.SetActive(ready);
        }
    }
}