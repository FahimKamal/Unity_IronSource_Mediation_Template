using UnityEngine;

public class MotherScript : MonoBehaviour
{
    [SerializeField] protected GameObject fadeScreen;
    
    protected void AdNotLoadedPopup()
    {
        PopupManager.Instance.ShowPopup("Notification", "Ad not loaded yet.");
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
