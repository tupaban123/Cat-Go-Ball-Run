using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInit : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
