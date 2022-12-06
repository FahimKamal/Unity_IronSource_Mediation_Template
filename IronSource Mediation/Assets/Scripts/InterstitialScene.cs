using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialScene : MotherScript
{
    private void Start()
    {
        FadeOut();
        IronSource.Agent.loadInterstitial();
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void AdShowedPopup()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSourceInterstitialEvents.onAdClosedEvent += OnInterstitialClosed;
            IronSource.Agent.showInterstitial();
        }
        else
        {
            AdNotLoadedPopup();
            IronSourceInterstitialEvents.onAdClosedEvent -= OnInterstitialClosed;
        }
    }
    
    private void OnInterstitialClosed(IronSourceAdInfo obj)
    {
        ShowPopup("Notification", "Ad was showed successfully.\nNew ad is now loading.");
        IronSource.Agent.loadInterstitial();
    }
    
    public void OnCheckButtonClicked()
    {
        var isReady = IronSource.Agent.isInterstitialReady();
        ShowPopup("Notification", isReady ? "Ad is ready to show." : "Ad is not ready to show.");
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
