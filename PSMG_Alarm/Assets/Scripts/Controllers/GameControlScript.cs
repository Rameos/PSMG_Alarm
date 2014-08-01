using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour
{
    public float maxNoGazeDataTime = 1;
    public bool blockWhenNoGazeData = true;

    public static float timeElapsed;

    private GameObject[] powerUps;
    private PowerUpSpawner powerUpSpawner;
    private PlayerShooting shooting;
    private MovePlayer movePlayer;
    private float noGazeDataTimer;
    private bool paused = false;

    public static int coins;

    void Start()
    {
        Screen.showCursor = false;
        timeElapsed = 0;

        coins = PlayerPrefsManager.GetCoins();
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().updateCoins(coins);

        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        shooting = GameObject.Find("gun").GetComponent<PlayerShooting>();
        powerUpSpawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!checkGazeDataAvailable() && blockWhenNoGazeData)
            noGazeDataTimer += Time.deltaTime;
        else
            noGazeDataTimer = 0;

        if (noGazeDataTimer > maxNoGazeDataTime)
            pauseGame();

        if (paused)
            waitForInput();
    }

    public void pauseGame()
    {
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        paused = true;

        movePlayer.stopPlayerMovement();
        shooting.blockShooting();
        stopEnemies();
        stopPowerUps();
    }

    public void unpauseGame()
    {
        startPowerUps();
        startEnemies();
        shooting.unblockShooting();
        movePlayer.startPlayerMovement();
        paused = false;
    }

    public void stopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
    }

    public void stopPowerUps()
    {
        powerUpSpawner.stopSpawning();
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        PowerUp powerUp;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUp = powerUps[i].GetComponent<PowerUp>();
            powerUp.stopPowerUpMovement();
        }
    }

    public void startEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
    }

    public void startPowerUps()
    {
        powerUpSpawner.stopSpawning();
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        PowerUp powerUp;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUp = powerUps[i].GetComponent<PowerUp>();
            powerUp.startPowerUpMovement();
        }
    }


    bool checkGazeDataAvailable()
    {
        return !(gazeModel.posGazeLeft.x == 0 && gazeModel.posGazeRight.x == 0);
    }

    void waitForInput()
    {
        if (Input.anyKey && checkGazeDataAvailable())
        {
            unpauseGame();
        }
    }

    public static void addCoins(int value)
    {
        coins += value;
        PlayerPrefsManager.SetCoins(coins);
    }
}
