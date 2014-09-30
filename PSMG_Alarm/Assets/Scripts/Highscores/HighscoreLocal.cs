using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreLocal : MonoBehaviour
{
    public Text scoreText;
    public Text nameText;
    public HighscoreElement[] highscores;

    void OnEnable()
    {
        highscores = PlayerPrefsManager.GetHighscore();
    }

    public void ShowScores()
    {
        scoreText.text = "";
        nameText.text = "";

        foreach (HighscoreElement highscore in highscores)
        {
            scoreText.text += highscore.GetScore() + "\n";
            nameText.text += highscore.GetName() + "\n";
        }
    }
}
