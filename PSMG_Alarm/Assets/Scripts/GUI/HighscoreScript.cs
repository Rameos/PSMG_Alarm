using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{

    public Text scoreText;
    public Text coinText;
    public Text ammoText;

    private int score;

    public void AddScoreValue(int value)
    {
        score = score + value;
        scoreText.text = "" + score;
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
}
