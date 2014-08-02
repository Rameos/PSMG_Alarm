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

    public override void Move()
    {
        base.Move();

        transform.Rotate(new Vector3(0, 2, 0));
    }
}
