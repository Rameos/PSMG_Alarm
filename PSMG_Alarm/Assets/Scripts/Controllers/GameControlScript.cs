using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour
{
    public float maxNoGazeDataTime = 1;
    public bool blockWhenNoGazeData = true;

    public static int coins;
    public static float timeElapsed;
    public float timeUntilLevelEnd;

    private GUIText countDown;
    private SubmarineLifeControl lifeControl;
    private GameObject[] powerUps;
    private PowerUpSpawner powerUpSpawner;
    private PlayerShooting shooting;
    private MovePlayer movePlayer;
    private float noGazeDataTimer;
    private bool paused = false;


    void Start()
    {
        Screen.showCursor = false;
        timeElapsed = 0;

        coins = PlayerPrefsManager.GetCoins();
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().updateCoins(coins);

        countDown = GameObject.Find("CountDown").GetComponent<GUIText>();
        lifeControl = GameObject.FindGameObjectWithTag("MainGUI").GetComponent<SubmarineLifeControl>();

        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        shooting = GameObject.Find("gun").GetComponent<PlayerShooting>();
        powerUpSpawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        timeUntilLevelEnd -= Time.deltaTime;

        countDown.text = ((int)timeUntilLevelEnd / 60).ToString() + ":" + ((int)timeUntilLevelEnd % 60).ToString();
        if (timeUntilLevelEnd <= 0)
        {
            SaveAndFinishLevel();
        }

        if (!CheckGazeDataAvailable() && blockWhenNoGazeData)
            noGazeDataTimer += Time.deltaTime;
        else
            noGazeDataTimer = 0;

        if (noGazeDataTimer > maxNoGazeDataTime)
            PauseGame();

        if (paused)
            WaitForInput();
    }

    private void SaveAndFinishLevel()
    {
        PlayerPrefsManager.SetCurrentLive(lifeControl.GetLifes());

        Application.LoadLevel("skilltree");
    }

    public void PauseGame()
    {
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        paused = true;

        movePlayer.stopPlayerMovement();
        shooting.blockShooting();
        StopEnemies();
        StopPowerUps();
    }

    public void UnpauseGame()
    {
        StartPowerUps();
        StartEnemies();
        shooting.unblockShooting();
        movePlayer.startPlayerMovement();
        paused = false;
    }

    public void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
    }

    public void StopPowerUps()
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

    public void StartEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
    }

    public void StartPowerUps()
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


    bool CheckGazeDataAvailable()
    {
        return !(gazeModel.posGazeLeft.x == 0 && gazeModel.posGazeRight.x == 0);
    }

    void WaitForInput()
    {
        if (Input.anyKey && CheckGazeDataAvailable())
        {
            UnpauseGame();
        }
    }

    public static void AddCoins(int value)
    {
        coins += value;
        PlayerPrefsManager.SetCoins(coins);
    }
}
