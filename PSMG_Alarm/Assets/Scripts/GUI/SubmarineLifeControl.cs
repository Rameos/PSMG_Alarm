using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmarineLifeControl : MonoBehaviour
{

    public GameObject[] sub;
    public GameObject[] sub_grey;
    public GameOverScript gameOverScript;

    private GameObject player;
    private int[] lifeArray = new int[6];
    private int life;
    private int maxLife;
    private GameObject cam2D;
    private GameObject cam3D;

    void Start()
    {
        cam2D = GameObject.FindGameObjectWithTag("MainCamera");
        cam3D = GameObject.FindGameObjectWithTag("3DCam");

        int currentLife = PlayerPrefsManager.GetCurrentLife();
        maxLife = PlayerPrefsManager.GetMaxLife();

        if (currentLife == 0)
        {
            PlayerPrefsManager.SetCurrentLive(GameConstants.PLAYER_START_HEALTH);
            PlayerPrefsManager.SetMaxLives(GameConstants.PLAYER_START_HEALTH);
            currentLife = GameConstants.PLAYER_START_HEALTH;
            maxLife = GameConstants.PLAYER_START_HEALTH;
        }

        life = currentLife;

        for (int i = 0; i < lifeArray.Length; i++)
        {
            if (i < currentLife)
                lifeArray[i] = 1;
        }

        for (int i = maxLife; i < lifeArray.Length; i++)
        {
            sub_grey[i].SetActive(false);
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

            cam2D.SendMessage("StartShaking", 0.8f);
            cam3D.SendMessage("StartShaking", 0.8f);
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
            if (lifeArray[i] != 1)
            {
                sub[i].SetActive(false);
            }
        }
    }

    public int GetLifes()
    {
        return life;
    }

}
