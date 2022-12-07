using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardedScene : MotherScript
{
    private void Start()
    {
        FadeOut();
        
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSource.Agent.loadRewardedVideo();
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += OnRewardedVideoAvailabilityChangedEvent;
    }

    private void OnRewardedVideoAvailabilityChangedEvent(bool obj)
    {
        ShowPopup("Notification", "Video is ready to play");
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    
    private void PlayRewardVideo(Action<IronSourcePlacement, IronSourceAdInfo> rewardAction)
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        if (isReady)
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent += rewardAction;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= rewardAction;
            AdNotLoadedPopup();
        }
    }

    public void On100RewardButtonClick()
    {
        PlayRewardVideo(Reward100Coins);
    }
    
    private void Reward100Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 100 coin.\nNew ad is now loading.");
        IronSource.Agent.loadRewardedVideo();
    }
    
    public void On200RewardButtonClick()
    {
        PlayRewardVideo(Reward200Coins);
    }
    
    private void Reward200Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 200 coin.\nNew ad is now loading.");
        IronSource.Agent.loadRewardedVideo();
    }
    
    public void On300RewardButtonClick()
    {
        PlayRewardVideo(Reward300Coins);
    }

    private void Reward300Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 300 coin.\nNew ad is now loading.");
        IronSource.Agent.loadRewardedVideo();
    }
    
    public void OnCheckButtonClick()
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        ShowPopup("Notification", isReady ? "Ad is ready to play" : "Ad is not ready to play");
    }
    
    public void OnBackButtonPressed()
    {
        StartCoroutine(OpenMainScene());
    }
    
    private IEnumerator OpenMainScene()
    {
        FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Main Scene");
    }
}
