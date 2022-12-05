using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialScene : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private GameObject fadeScreen;

    private void Start()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeOut();
        IronSource.Agent.loadInterstitial();
    }

    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }


    private void AdNotLoadedPopup()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad not loaded yet.");
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
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad was showed successfully.\nNew ad is now loading.");
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
