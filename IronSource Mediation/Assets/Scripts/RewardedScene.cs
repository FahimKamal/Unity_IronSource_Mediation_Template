using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardedScene : MotherScript
{
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        CallBackManager.Instance.onCoinCollected.AddListener(UpdateCoinText);
    }
    
    private void OnDisable()
    {
        CallBackManager.Instance.onCoinCollected.RemoveListener(UpdateCoinText);
    }

    private void Start()
    {
        FadeOut();
        
        AdManager.Instance.LoadRewardedAd();

        if (PlayerPrefs.HasKey("Coins"))
        {
            UpdateCoinText();
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 300);
            UpdateCoinText();
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = "COIN: " + PlayerPrefs.GetInt("Coins");
    }
  
    private void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    
    public void On100RewardButtonClick()
    {
        AdManager.Instance.PlayRewardedAdVideo(Reward100Coins);
    }
    
    private void Reward100Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        var coins = PlayerPrefs.GetInt("Coins");
        coins += 100;
        PlayerPrefs.SetInt("Coins", coins);
        CallBackManager.Instance.onCoinCollected?.Invoke();
        
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 100 coin.\nNew ad is now loading.");
        AdManager.Instance.LoadRewardedAd();
    }
    
    public void On200RewardButtonClick()
    {
        AdManager.Instance.PlayRewardedAdVideo(Reward200Coins);
    }
    
    private void Reward200Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        var coins = PlayerPrefs.GetInt("Coins");
        coins += 200;
        PlayerPrefs.SetInt("Coins", coins);
        CallBackManager.Instance.onCoinCollected?.Invoke();
        
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 200 coin.\nNew ad is now loading.");
        AdManager.Instance.LoadRewardedAd();
    }
    
    public void On300RewardButtonClick()
    {
        AdManager.Instance.PlayRewardedAdVideo(Reward300Coins);
    }

    private void Reward300Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        var coins = PlayerPrefs.GetInt("Coins");
        coins += 300;
        PlayerPrefs.SetInt("Coins", coins);
        CallBackManager.Instance.onCoinCollected?.Invoke();
        
        ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 300 coin.\nNew ad is now loading.");
        AdManager.Instance.LoadRewardedAd();
    }
    
    public void OnCheckButtonClick()
    {
        var isReady = AdManager.Instance.IsRewardedAdReady();
        ShowPopup("Notification", isReady ? "Ad is ready to play" : "Ad is not ready to play");
    }
    
    public void OnResetCoinsButtonClick()
    {
        ShowPopup("Notification", "Coin has been reset to 300");
        PlayerPrefs.SetInt("Coins", 300);
        UpdateCoinText();
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
