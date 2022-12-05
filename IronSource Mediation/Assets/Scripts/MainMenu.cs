using System;
using System.Collections;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private GameObject fadeScreen;
    
    [SerializeField] private string ironSourceAppKey = "17a4eab05";

    private void Start()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeOut();
        IronSource.Agent.init (ironSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);

        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

        IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
    }

    private void OnBannerAdLoadedEvent()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Banner ad is successfully loaded.");
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    
    public void OnBannerOnButtonClick()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Banner", "You will see the test banner after this screen goes away.");
    }
    
    public void OnBannerOffButtonClick()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Banner", "Test banner will now hide after this screen goes away.");
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
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        Application.Quit();
    }

    private IEnumerator OpenRewardedScene()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Rewarded Ad Scene");
    }

    private IEnumerator OpenInterstitialScene()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Scenes/Interstitial Scene");
    }

}
