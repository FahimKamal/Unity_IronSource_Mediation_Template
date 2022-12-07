using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MotherScript
{

    [SerializeField] private string ironSourceAppKey = "17a4eab05";

    private void Awake()
    {
        IronSource.Agent.init (ironSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);

        if (GameObject.FindObjectOfType<CallBackManager>() == null)
        {
            var callBackManager = new GameObject("CallBackManager");
            callBackManager.AddComponent<CallBackManager>();
        }
    }

    private void Start()
    {
        FadeOut();

        LoadBanner();
    }


    private void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
        IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
    }
    
    private static void ShowBanner()
    {
        IronSource.Agent.displayBanner();
    }
    
    private static void HideBanner()
    {
        
        IronSource.Agent.hideBanner();
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

        CallBackManager.Instance.onPopupClosed.RemoveAllListeners();
        CallBackManager.Instance.onPopupClosed.AddListener(ShowBanner);
    }
    
    public void OnBannerOffButtonClick()
    {
        ShowPopup("Notification", "Test banner will now hide after this screen goes away.");
        
        CallBackManager.Instance.onPopupClosed.RemoveAllListeners();
        CallBackManager.Instance.onPopupClosed.AddListener(HideBanner);
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
