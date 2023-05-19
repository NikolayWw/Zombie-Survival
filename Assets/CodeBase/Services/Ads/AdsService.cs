using CodeBase.Data;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5262726";
        private const string IOSGameId = "5262727";

        private const string RewardedPlacementIdAndroid = "Rewarded_Android";
        private const string RewardedPlacementIdIOS = "Rewarded_iOS";

        public Action RewardedVideoReady { get; set; }

        private Action _onVideoFinished;

        private string _gameId = string.Empty;
        private string _placementI = string.Empty;

        public bool AdsReady { get; private set; } = true;

        public void Initialize()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _gameId = AndroidGameId;
                _placementI = RewardedPlacementIdAndroid;
            }
            else
            {
                _gameId = IOSGameId;
                _placementI = RewardedPlacementIdIOS;
            }

            Advertisement.Initialize(_gameId, GameConstants.IsAdsTestMod, this);
            Advertisement.Load(_placementI, this);
        }

        public void ShowRewarded(Action onVideoFinished)
        {
            AdsReady = false;
            _onVideoFinished = onVideoFinished;
            Advertisement.Show(_placementI, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            AdsReady = true;
            RewardedVideoReady?.Invoke();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinished?.Invoke();
                    break;

                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogError("OnUnityAdsShowComplete " + showCompletionState);
                    break;

                default:
                    break;
            }

            Advertisement.Load(_placementI, this);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            AdsReady = false;
            Debug.LogError($"OnUnityAdsFailedToLoad {error } {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"OnUnityAdsShowFailure {error } {message}");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            AdsReady = false;
            Debug.LogError($"OnInitializationFailed {error } {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        { }

        public void OnUnityAdsShowClick(string placementId)
        { }

        public void OnInitializationComplete()
        { }
    }
}