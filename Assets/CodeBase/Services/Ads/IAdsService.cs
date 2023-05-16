using System;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        Action RewardedVideoReady { get; set; }
         bool AdsReady { get; }
        void ShowRewarded(Action onVideoFinished);
        void Initialize();

    }
}