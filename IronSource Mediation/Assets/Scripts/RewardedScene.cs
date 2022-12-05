using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardedScene : MonoBehaviour
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

    public void On100RewardButtonClick()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad was showed successfully and you have been rewarded 100 coin.\nNew ad is now loading.");
    }
    
    public void On200RewardButtonClick()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad was showed successfully and you have been rewarded 200 coin.\nNew ad is now loading.");
    }
    
    public void On300RewardButtonClick()
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData("Notification", "Ad was showed successfully and you have been rewarded 300 coin.\nNew ad is now loading.");
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
