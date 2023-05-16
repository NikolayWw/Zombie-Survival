using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsShowListener
    {
        private const string AndroidGameId = "5262726";
        private const string IOSGameId = "5262727";

        private const string RewardedPlacementIdAndroid = "Rewarded_Android";
        private const string RewardedPlacementIdIOS = "Rewarded_iOS";

        public Action RewardedVideoReady { get; set; }

        private Action _onVideoFinished;

        private string _gameId = string.Empty;
        private string _placementI = string.Empty;

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

            Advertisement.Initialize(_gameId, true, this);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(_placementI, this);
            _onVideoFinished = onVideoFinished;
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == _placementI)
                RewardedVideoReady?.Invoke();
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads initialize Complete");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"OnInitializationFailed {error} message:{message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        { }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"OnUnityAdsShowFailure {error} message:{message}");
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
                    Debug.LogError($"OnUnityAdsShowComplete {showCompletionState}");
                    break;

                default:
                    break;
            }

            _onVideoFinished = null;
        }
    }
}