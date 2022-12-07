using System;
using UnityEngine;

// Replace MotherScript with MonoBehaviour if you want to use this script as a standalone script
public class AdManager : MotherScript
{
    public static AdManager Instance;
    
    [SerializeField] private string ironSourceAppKey = "17a4eab05";

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            IronSource.Agent.init (ironSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }

    #region Banner

    public void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
        
        IronSourceEvents.ResetOnBannerAdLoadedEvent();
        IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
    }
    
    public void ShowBanner()
    {
        IronSource.Agent.displayBanner();
    }
    
    public void HideBanner()
    {
        
        IronSource.Agent.hideBanner();
    }

    // Replace this method with your own logic or remove it completely
    private void OnBannerAdLoadedEvent()
    {
        ShowPopup("Notification", "Banner ad is successfully loaded.");
    }

    #endregion

    #region Interstitial Ad

    /// <summary>
    /// Load interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        IronSource.Agent.loadInterstitial();
    }

    /// <summary>
    /// Show interstitial ad if it is loaded.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSourceInterstitialEvents.ResetOnAdClosedEvent();
            IronSourceInterstitialEvents.onAdClosedEvent += OnInterstitialClosed;
            
            IronSource.Agent.showInterstitial();
        }
        else
        {
            // Replace this method call with your own logic or remove it.
            AdNotLoadedPopup();
            
            IronSourceInterstitialEvents.ResetOnAdClosedEvent();
        }
    }
    
    private void OnInterstitialClosed(IronSourceAdInfo obj)
    {
        // Replace this method call with your own logic or remove it completely
        ShowPopup("Notification", "Ad was showed successfully.\nNew ad is now loading.");
        
        // Not this method call
        LoadInterstitialAd();
    }
    
    
    /// <summary>
    /// Check if interstitial ad is ready to play.
    /// </summary>
    /// <returns>True if ad Available. False if not available.</returns>
    public bool IsInterstitialAdReady()
    {
        return IronSource.Agent.isInterstitialReady();
    }
    
    #endregion

    #region Rewarded Ad

    /// <summary>
    /// Load rewarded Ad video.
    /// </summary>
    public void LoadRewardedAd()
    {
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSource.Agent.loadRewardedVideo();
        
        IronSourceEvents.ResetOnRewardedVideoAvailabilityChangedEvent();
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += OnRewardedVideoAvailabilityChangedEvent;
    }
    
    // Rewrite this method with your own logic or remove it completely
    private void OnRewardedVideoAvailabilityChangedEvent(bool obj)
    {
        ShowPopup("Notification", "Video is ready to play");
    }
    
    /// <summary>
    /// Play rewarded Ad video on demand.
    /// </summary>
    /// <param name="rewardAction">Action to perform after user successfully watch ad video.</param>
    public void PlayRewardedAdVideo(Action<IronSourcePlacement, IronSourceAdInfo> rewardAction)
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        if (isReady)
        {
            IronSourceRewardedVideoEvents.ResetOnAdRewardedEvent();
            IronSourceRewardedVideoEvents.onAdRewardedEvent += rewardAction;
            
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            IronSourceRewardedVideoEvents.ResetOnAdRewardedEvent();
            AdNotLoadedPopup();
        }
    }

    /// <summary>
    /// Check if rewarded Ad video is ready to play.
    /// </summary>
    /// <returns>True if video Available. False if not available.</returns>
    public bool IsRewardedAdReady()
    {
        return IronSource.Agent.isRewardedVideoAvailable();
    }

    #endregion
    
}