using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CheckHighscoreValue : MonoBehaviour
{
    private bool disabled = false;
    public Text scoreText;
    public InputField nameText;

    void OnEnable()
    {
        int score = Convert.ToInt32(scoreText.text);

        if (disabled)
        {
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        HighscoreElement[] highscores = PlayerPrefsManager.GetHighscore();
        Debug.Log(highscores.Length);
        Debug.Log(highscores[highscores.Length - 1].GetScore());
        Debug.Log(scoreText.text);
        gameObject.GetComponent<Button>().interactable = (score > highscores[highscores.Length - 1].GetScore());
    }

    public void Disable()
    {
        disabled = true;
    }

    public void StoreScore()
    {
        int score = Convert.ToInt32(scoreText.text);
        string name = nameText.value;

        HighscoreElement[] highscores = PlayerPrefsManager.GetHighscore();
        HighscoreElement preScoreBuffer = null;
        HighscoreElement scoreBuffer = null;
        int i = 0;

        for (int y = 0; y < highscores.Length; y++)
        {
            if (score > highscores[y].GetScore())
            {
                scoreBuffer = new HighscoreElement(highscores[y].GetName(), highscores[y].GetScore());
                highscores[y].SetScore(score);
                highscores[y].SetName(name);
                i = y + 1;
                break;
            }
        }

        for (int y = i; y < highscores.Length; y++)
        {
            preScoreBuffer = new HighscoreElement(highscores[y].GetName(), highscores[y].GetScore());
            highscores[y].SetScore(scoreBuffer.GetScore());
            highscores[y].SetName(scoreBuffer.GetName());
            scoreBuffer = new HighscoreElement(preScoreBuffer.GetName(), preScoreBuffer.GetScore());
        }
        PlayerPrefsManager.SetHighscore(highscores);
    }
}