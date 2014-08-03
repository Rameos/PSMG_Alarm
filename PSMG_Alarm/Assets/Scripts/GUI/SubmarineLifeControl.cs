using UnityEngine;
using System.Collections;

public class SubmarineLifeControl : MonoBehaviour
{

    public GUITexture[] sub = new GUITexture[4];
    public GameOverScript gameOverScript;
    public Texture2D red;
    public Texture2D grey;

    private GameObject player;
    private int[] lifeArray = new int[4];
    private int life;
    void Start()
    {
        int currentLife = PlayerPrefsManager.GetCurrentLife();

        if (currentLife == 0)
        {
            PlayerPrefsManager.SetCurrentLive(GameConstants.PLAYER_START_HEALTH);
            PlayerPrefsManager.SetMaxLives(GameConstants.PLAYER_START_HEALTH);
            currentLife = GameConstants.PLAYER_START_HEALTH;
        }

        life = currentLife;

        for (int i = 0; i < lifeArray.Length; i++)
        {
            if (i < currentLife)
                lifeArray[i] = 1;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        UpdateLife();
    }

    public void IncrementLife()
    {
        if (life > 0 && life < 4)
        {
            life++;
            lifeArray[life - 1] = 1;
            UpdateLife();
        }
    }

    public void DecrementLife()
    {
        if (life > 0 && life <= 4)
        {
            life--;
            lifeArray[life] = 0;
            UpdateLife();

            player.SendMessage("LightAnimation");
        }
        if (life <= 0)
        {
            gameOverScript.EndOfGame();
        }
    }

    void UpdateLife()
    {

        for (int i = 0; i < lifeArray.Length; i++)
        {
            if (lifeArray[i] == 1)
            {
                sub[i].texture = red;
            }
            else
            {
                sub[i].texture = grey;
            }
        }
    }

    public int GetLifes()
    {
        return life;
    }

}
