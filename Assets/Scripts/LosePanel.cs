using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanel : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private GameObject newImage;
    [SerializeField] private Image medalImage;

    [Header("Medals")] 
    [SerializeField] [Tooltip("Указывать медальки в порядке спадания, то есть от большего к меньшему")] 
    private List<MedalsPair> medalsPairs = new List<MedalsPair>();

    [Inject] private Pouch pouch;

    public void Init(int score)
    {
        if(score > pouch.BestScore)
        {
            newImage.SetActive(true);
            pouch.SetBestScore(score);
        }
        
        var bestScore = pouch.BestScore;

        char[] scoreChars = score.ToString().ToCharArray();
        char[] bestScoreChars = bestScore.ToString().ToCharArray();
        
        string scoreText = "";
        string bestScoreText = "";

        foreach (var scoreChar in scoreChars)
        {
            scoreText += pouch.GetSpriteText(scoreChar);
        }
        
        foreach (var scoreChar in bestScoreChars)
        {
            bestScoreText += pouch.GetSpriteText(scoreChar);
        }

        currentScore.text = scoreText;
        this.bestScore.text = bestScoreText;

        CalculateMedal(score);
    }

    private void CalculateMedal(int score)
    {
        foreach (var medalPair in medalsPairs)
        {
            if (score >= medalPair.Score)
            {
                medalImage.sprite = medalPair.Medal;
                
                if(medalPair.Medal != null)
                    medalImage.color = new Color(255, 255, 255, 1);
                else
                    medalImage.color = new Color(255, 255, 255, 0);

                return;
            }
        }
    }
}

[Serializable]
public class MedalsPair
{
    public int Score;
    public Sprite Medal;
}
