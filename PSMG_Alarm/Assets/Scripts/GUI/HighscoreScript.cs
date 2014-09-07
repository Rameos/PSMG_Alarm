using UnityEngine;
using System.Collections;

public class HighscoreScript : MonoBehaviour
{

    public GUIText scoreText;
    public GUIText coinText;
    public GUIText ammoText;

    private int score;

    public void AddScoreValue(int value)
    {
        score = score + value;
        ChangeScoreText();
    }
    public void UpdateCoins(int coins)
    {
        coinText.text = "x " + coins;
    }

    public void UpdateAmmo(int ammo)
    {
        if (ammo > -1)
        {
            ammoText.text = "Ammo: " + ammo;
        }
        else
        {
            ammoText.text = "Ammo: --";
        }
    }

    void ChangeScoreText()
    {
        scoreText.text = "" + score;
    }
}
