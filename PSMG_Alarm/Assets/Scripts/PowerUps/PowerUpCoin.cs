using UnityEngine;
using System.Collections;

public class PowerUpCoin : PowerUp 
{
    public int value;

    public override void ApplyPowerUp()
    {
        GameControlScript.addCoins(value);
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().updateCoins(PlayerPrefsManager.GetCoins());
    }
}
