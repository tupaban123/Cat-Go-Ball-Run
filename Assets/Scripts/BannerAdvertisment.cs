using GoogleMobileAds.Api;
using UnityEngine;

public class BannerAdvertisment : MonoBehaviour
{
    [SerializeField] private string adIDAndroid;
    
    private BannerView bannerView;

    public void Start()
    {
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = adIDAndroid;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}
