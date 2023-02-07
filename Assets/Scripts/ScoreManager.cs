using System;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public int currentScore { get; private set; }

    [Inject] private Pouch pouch;

    public void AddScore()
    {
        currentScore++;
        UpdateText();
        
        if(currentScore >= 999)
            FindObjectOfType<Cat>().StartLose();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            AddScore();
    }

    private void UpdateText()
    {
        var scoreString = currentScore.ToString();
        char[] scoreNumbers = scoreString.ToCharArray();

        string scoreToText = "";
        
        foreach (var scoreNumber in scoreNumbers)
        {
            scoreToText += pouch.GetSpriteText(scoreNumber);
        }

        scoreText.text = scoreToText;
    }
}
