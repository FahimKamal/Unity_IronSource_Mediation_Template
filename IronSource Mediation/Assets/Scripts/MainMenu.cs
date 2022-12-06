using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MotherScript
{

    [SerializeField] private string ironSourceAppKey = "17a4eab05";

    private void Awake()
    {
        IronSource.Agent.init (ironSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
    }

    private void Start()
    {
        FadeOut();

        LoadBanner();
    }
    
    public void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
    }

    private void OnBannerAdLoadedEvent()
    {
        ShowPopup("Notification", "Banner ad is successfully loaded.");
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    
    public void OnBannerOnButtonClick()
    {
        ShowPopup("Notification", "You will see the test banner after this screen goes away.");
    }
    
    public void OnBannerOffButtonClick()
    {
        ShowPopup("Notification", "Test banner will now hide after this screen goes away.");
    }

    public void OnInterstitialButtonClick()
    {
        StartCoroutine(OpenInterstitialScene());
    }
    
    public void OnRewardedButtonClick()
    {
        StartCoroutine(OpenRewardedScene());
    }
    
    public void OnExitButtonClick()
    {
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        FadeIn();
        yield return new WaitForSeconds(1.2f);
        Application.Quit();
    }

    private IEnumerator OpenRewardedScene()
    {
        FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Rewarded Ad Scene");
    }

    private IEnumerator OpenInterstitialScene()
    {
        FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Interstitial Scene");
    }

}
