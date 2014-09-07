using UnityEngine;
using System.Collections;

public class PowerUpCoin : PowerUp 
{
    public int value;

    public override void ApplyPowerUp()
    {
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
        GameControlScript.AddCoins(value);
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().UpdateCoins(PlayerPrefsManager.GetCoins());
    }

    public override void Move()
    {
        base.Move();

        transform.Rotate(new Vector3(0, 2, 0));
    }
}
