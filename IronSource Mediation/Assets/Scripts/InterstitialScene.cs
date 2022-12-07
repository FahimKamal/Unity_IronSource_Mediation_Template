using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialScene : MotherScript
{
    private void Start()
    {
        FadeOut();
        AdManager.Instance.LoadInterstitialAd();
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void ShowAd()
    {
        AdManager.Instance.ShowInterstitialAd();
    }
    
    public void OnCheckButtonClicked()
    {
        var isReady = AdManager.Instance.IsInterstitialAdReady();
        ShowPopup("Notification", isReady ? "Ad is ready to show." : "Ad is not ready to show.");
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
