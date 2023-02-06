using System;
using System.Collections.Generic;
using UnityEngine;

public class Pouch : MonoBehaviour
{
    [SerializeField] private List<SpritesIndexesKeys> indexesPairs = new List<SpritesIndexesKeys>();

    private Dictionary<char, int> indexes = new Dictionary<char, int>();

    public static Pouch Instance;

    private InterstitialAdvertisment _ad;

    public int BestScore { get; private set; }
    public int LosesCount { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("BestScore"))
            BestScore = PlayerPrefs.GetInt("BestScore");
        else
        {
            BestScore = 0;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }

        foreach (var item in indexesPairs)
        {
            indexes[item.Number] = item.SpriteIndex;
        }

        _ad = FindObjectOfType<InterstitialAdvertisment>();

        Debug.Log(_ad != null);
    }

    public void SetBestScore(int value)
    {
        BestScore = value;
        PlayerPrefs.SetInt("BestScore", BestScore);
    }

    public void AddLose()
    {
        LosesCount++;
        Debug.Log(LosesCount);
        
        if (LosesCount >= 2)
        {
            LosesCount = 0;
            
            if(_ad != null)
                _ad.EnableAd();
        }
    }
    
    public string GetSpriteText(char number)
    {
        return $"<sprite index={indexes[number]}>";
    }
}

[Serializable]
public class SpritesIndexesKeys
{
    public char Number;
    public int SpriteIndex;
}
