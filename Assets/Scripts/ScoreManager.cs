using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public int currentScore { get; private set; }

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
        var pouch = Pouch.Instance;

        string scoreToText = "";
        
        foreach (var scoreNumber in scoreNumbers)
        {
            scoreToText += pouch.GetSpriteText(scoreNumber);
        }

        scoreText.text = scoreToText;
    }
}
