using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControlScript : MonoBehaviour
{
    public float maxNoGazeDataTime = 1;
    public bool blockWhenNoGazeData = true;
    public string sceneWhenFinished;

    public static int coins;
    public static int score;
    public static float timeElapsed;
    public float timeUntilLevelEnd;
    public Text countDown;

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
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().UpdateCoins(coins);
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().AddScoreValue(PlayerPrefsManager.GetScore());

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
        if (timeUntilLevelEnd <= 0 && lifeControl.GetLifes() > 0)
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
        PlayerPrefsManager.SetScore(score);
        shooting.SaveAmmo();

        Application.LoadLevel(sceneWhenFinished);
    }

    public void PauseGame()
    {
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        paused = true;

        movePlayer.stopPlayerMovement();
        shooting.BlockShooting();
        StopEnemies();
        StopPowerUps();
    }

    public void UnpauseGame()
    {
        StartPowerUps();
        StartEnemies();
        shooting.UnblockShooting();
        movePlayer.startPlayerMovement();
        paused = false;
    }

    public void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.StopEnemyMovement();
        }
    }

    public void StopPowerUps()
    {
        powerUpSpawner.StopSpawning();
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        PowerUp powerUp;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUp = powerUps[i].GetComponent<PowerUp>();
            powerUp.StopPowerUpMovement();
        }
    }

    public void StartEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.StopEnemyMovement();
        }
    }

    public void StartPowerUps()
    {
        powerUpSpawner.StopSpawning();
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        PowerUp powerUp;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUp = powerUps[i].GetComponent<PowerUp>();
            powerUp.StartPowerUpMovement();
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
