using UnityEngine;

public class MotherScript : MonoBehaviour
{
    [SerializeField] protected GameObject popupPrefab;
    [SerializeField] protected GameObject fadeScreen;

    protected void ShowPopup(string title, string description)
    {
        var popup = Instantiate(popupPrefab, transform);
        popup.GetComponent<SetPopup>().SetPopupData(title, description);
    }
    
    protected void AdNotLoadedPopup()
    {
        ShowPopup("Notification", "Ad not loaded yet.");
    }

    protected void FadeIn()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeIn();
    }
    
    protected void FadeOut()
    {
        Instantiate(fadeScreen, transform).GetComponent<FadeScreenController>().FadeOut();
    }
}
