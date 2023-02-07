using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAdvertisment : MonoBehaviour
{
    [SerializeField] private string adIdAndroid;
    
    private InterstitialAd interstitial;

    private void Start()
    {
        RequestInterstitial();
    }

    public void EnableAd()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    public void RequestInterstitial()
    {
        MobileAds.Initialize(initStatus => { });
        
#if UNITY_ANDROID
        string adUnitId = adIdAndroid;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
}
