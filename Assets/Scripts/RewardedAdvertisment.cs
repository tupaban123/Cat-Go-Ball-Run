using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedAdvertisment : MonoBehaviour
{
    [SerializeField] private string adUnitId;
    
    private RewardedAd rewardedAd;

    private void Start()
    {
        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    
    public void EnableAd()
    {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }
}
