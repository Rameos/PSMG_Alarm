using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckHighscoreValue : MonoBehaviour
{
    private bool disabled = false;

    void OnEnable()
    {
        if (disabled)
        {
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        HighscoreElement[] highscores = PlayerPrefsManager.GetHighscore();
        gameObject.GetComponent<Button>().interactable = (GameControlScript.score > highscores[highscores.Length - 1].GetScore());
    }

    public void Disable()
    {
        disabled = true;
    }

    public void StoreScore(string name)
    {
        HighscoreElement[] highscores = PlayerPrefsManager.GetHighscore();
        HighscoreElement preScoreBuffer = null;
        HighscoreElement scoreBuffer = null;
        int i = 0;

        for (int y = 0; y < highscores.Length; y++)
        {
            if (GameControlScript.score > highscores[y].GetScore())
            {
                scoreBuffer = new HighscoreElement(highscores[y].GetName(), highscores[y].GetScore());
                highscores[y].SetScore(GameControlScript.score);
                highscores[y].SetName(name);
                i = y + 1;
                break;
            }
        }

        for (int y = i; y < highscores.Length; y++)
        {
            preScoreBuffer = highscores[y];
            highscores[y].SetScore(scoreBuffer.GetScore());
            highscores[y].SetName(scoreBuffer.GetName());
            scoreBuffer = preScoreBuffer;
        }
        PlayerPrefsManager.SetHighscore(highscores);
    }
}