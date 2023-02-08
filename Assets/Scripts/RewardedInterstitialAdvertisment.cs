using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedInterstitialAdvertisment : MonoBehaviour
{
    [SerializeField] private string adUnitIdAndroid;
    
    private RewardedInterstitialAd rewardedInterstitialAd;

    public void Start()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd(adUnitIdAndroid, request, adLoadCallback);
    }
    
    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAd = ad;
        }
    }   
    
    public void EnableAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
    }
}