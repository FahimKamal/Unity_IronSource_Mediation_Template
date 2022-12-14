# Unity_IronSource_Mediation_Template
Template to implement ironSource mediation in a Unity game with sample UI. It's very simple to use, just add the prefab in your main scene. Now no matter on which scene you open in your game, that gameobject will always be active. See usage below for better understanding.

#### Note: it only works for android. iOS's integration will be added later on.

#### Warning: Don't update ironSource adapter version from 7.2.5.1 to any latest version. There are some problems. If you do and solve the issues, feel free to contribute in this repo.

## Usage

+ <strong>Initialization</strong> <br>
Get IronSource app key from your IronSource dashboard. Paste it in app key box. Select the ads that you want to show in your app. For banner ad, select the position. <br><br>
![Initialization](https://imgur.com/j3rlM6u.gif "Initialization")

+ <strong>Show Banner</strong> <br>
If you initialize Banner ad. The ad will automatically load and display on your screen, but if you want better control than you can use the below method in your code.
```c#
    // Show Banner ad on screen
    AdManager.Instance.ShowBanner();

    // Hide Banner ad on screen
    AdManager.Instance.HideBanner();
```

+ <strong>Show Interstitial Ad</strong> <br>
The ad will be loaded at the beginning of the game. If loaded successfully bellow code will show ad, otherwise nothing will happen and game will continue. After successfully showing ad, next will be automatically loaded. 
```c#
    AdManager.Instance.ShowInterstitialAd();
```
If you want to check if the ad is loaded or not, you can use this code.
```c#
    AdManager.Instance.IsInterstitialAdReady();
```

+ <strong>Show Rewarded Ad</strong> <br>
The ad will be loaded at the beginning of the game. If loaded successfully bellow code will show ad, otherwise nothing will happen and game will continue. After successfully showing ad, next will be automatically loaded. 
```c#
    AdManager.Instance.PlayRewardedAdVideo(Reward100Coins);
```
To reward the player after showing the rewarded ad, you have to create a void method and pass that method as parameter while calling the `PlayRewardedAdVideo();` method.
```c#
    private void Reward100Coins(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo)
    {
        var coins = PlayerPrefs.GetInt("Coins");
        coins += 100;
        PlayerPrefs.SetInt("Coins", coins);
        CallBackManager.Instance.onCoinCollected?.Invoke();

        PopupManager.Instance.ShowPopup("Notification",
            "Ad was showed successfully and you have been rewarded 100 coin.\nNew ad is now loading.");
        AdManager.Instance.LoadRewardedAd();
    }
```
If you want to check if the ad is loaded or not, you can use this code.
```c#
    AdManager.Instance.IsRewardedAdReady();
```

## Summary
I only added those that I needed for my projects, If you need more there is lot more where it came from see IronSource Documentation
[here.](https://developers.is.com/ironsource-mobile/unity/unity-plugin/ "IronSouce Docs")
