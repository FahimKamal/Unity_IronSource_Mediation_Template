using System;
using UnityEngine;

// Replace MotherScript with MonoBehaviour if you want to use this script as a standalone script
public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    
    [SerializeField] private string ironSourceAppKey = "17bb14b55";
    
    [Header("Initialization")]
    [SerializeField] private bool bannerEnabled;
    [SerializeField] private bool interstitialEnabled;
    [SerializeField] private bool rewardedVideoEnabled;

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            IronSource.Agent.init (ironSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
            IronSource.Agent.validateIntegration();
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
    #endregion

    private void Start()
    {
        LoadAds();
    }

    private void LoadAds()
    {
        if (bannerEnabled)
        {
            LoadBanner();
        }

        if (interstitialEnabled)
        {
            LoadInterstitialAd();
        }
        
        if (rewardedVideoEnabled)
        {
            LoadRewardedAd();
        }
    }
    
    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    
    private static void AdNotLoadedPopup()
    {
        PopupManager.Instance.ShowPopup("Notification", "Ad not loaded yet.");
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
        PopupManager.Instance.ShowPopup("Notification", "Banner ad is successfully loaded.");
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
        PopupManager.Instance.ShowPopup("Notification", "Ad was showed successfully.\nNew ad is now loading.");
        
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
        PopupManager.Instance.ShowPopup("Notification", "Video is ready to play");
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
            IronSourceRewardedVideoEvents.onAdRewardedEvent += LoadNextAd;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += rewardAction;
            
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            AdNotLoadedPopup();
        }
    }
    
    private void LoadNextAd(IronSourcePlacement obj, IronSourceAdInfo arg2)
    {
        // Replace this method call with your own logic or remove it completely
        PopupManager.Instance.ShowPopup("Notification", "Ad was showed successfully.\nNew ad is now loading.");
        
        // Not this method call
        LoadRewardedAd();
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