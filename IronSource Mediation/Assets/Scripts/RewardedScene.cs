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

    public void On100RewardButtonClick()
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        if (isReady)
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent += Reward100Coins;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= Reward100Coins;
            AdNotLoadedPopup();
        }
        
    }
    
    private void Reward100Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 100 coin.\nNew ad is now loading.");
        IronSource.Agent.loadRewardedVideo();
    }
    
    public void On200RewardButtonClick()
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        if (isReady)
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent += Reward200Coins;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= Reward200Coins;
            AdNotLoadedPopup();
        }
    }
    
    private void Reward200Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 200 coin.\nNew ad is now loading.");
        IronSource.Agent.loadRewardedVideo();
    }
    
    public void On300RewardButtonClick()
    {
        var isReady = IronSource.Agent.isRewardedVideoAvailable();
        if (isReady)
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent += Reward300Coins;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= Reward300Coins;
            AdNotLoadedPopup();
        }
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
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Main Scene");
    }
}
