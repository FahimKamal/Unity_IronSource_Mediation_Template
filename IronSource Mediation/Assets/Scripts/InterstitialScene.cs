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
    }

    public void AdNotLoadedPopup()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad not loaded yet.");
    }
    
    public void AdShowedPopup()
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
