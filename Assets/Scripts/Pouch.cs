using System;
using System.Collections.Generic;
using UnityEngine;

public class Pouch : MonoBehaviour
{
    [SerializeField] private List<SpritesIndexesKeys> indexesPairs = new List<SpritesIndexesKeys>();

    private Dictionary<char, int> indexes = new Dictionary<char, int>();

    private InterstitialAdvertisment _ad;

    public int BestScore { get; private set; }

    private void Awake()
    {
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
    }

    public void SetBestScore(int value)
    {
        BestScore = value;
        PlayerPrefs.SetInt("BestScore", BestScore);
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
