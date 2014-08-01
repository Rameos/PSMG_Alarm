using UnityEngine;
using System.Collections;

public class HighscoreScript : MonoBehaviour
{

    public GUIText scoreText;
    public GUIText coinText;

    private int score;

    public void addScoreValue(int value)
    {
        score = score + value;
        changeScoreText();
    }
    public void updateCoins(int coins)
    {
        coinText.text = "x " + coins;
    }

    void changeScoreText()
    {
        scoreText.text = "" + score;
    }

}
