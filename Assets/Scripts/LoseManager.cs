using System;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public int LosesCount { get; private set; } = 0;

    [SerializeField] private int loseCount;
    [SerializeField] private RewardedInterstitialAdvertisment _ad;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LosesCount"))
        {
            LosesCount = PlayerPrefs.GetInt("LosesCount");
        }
        else
        {
            LosesCount = 0;
            PlayerPrefs.SetInt("LosesCount", LosesCount);    
        }
    }

    public void AddLose()
    {
        LosesCount++;
        Debug.Log(LosesCount);
        
        if (LosesCount >= loseCount)
        {
            LosesCount = 0;
            
            if(_ad != null)
                _ad.EnableAd();
        }
        PlayerPrefs.SetInt("LosesCount", LosesCount);
    }
}
