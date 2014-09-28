using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreLocal : MonoBehaviour 
{
    public Text scoreText;
    public Text nameText;

    void OnEnable()
    {
        HighscoreElement[] highscores = PlayerPrefsManager.GetHighscore();
        ShowScores(highscores);
    }

    private void ShowScores(HighscoreElement[] highscores)
    {
        scoreText.text = "";
        nameText.text = "";
        
        foreach(HighscoreElement highscore in highscores)
        {
            scoreText.text += highscore.GetScore() + "\n";
            nameText.text += highscore.GetName() + "\n";
        }
    }
}
