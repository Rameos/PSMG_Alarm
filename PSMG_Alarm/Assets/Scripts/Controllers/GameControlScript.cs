using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControlScript : MonoBehaviour
{
    public float maxNoGazeDataTime = 1;
    public bool blockWhenNoGazeData = true;
    public string sceneWhenFinished;
    public GameObject pausePanel;
    public GameObject noGazeDataText;

    public static int coins;
    public static int score;
    public static float timeElapsed;
    public float timeUntilLevelEnd;
    public Text countDown;
    public string theNextLevel;

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
        blockWhenNoGazeData = PlayerPrefsManager.GetControl();
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().UpdateCoins(coins);
        GameObject.Find("Highscore").GetComponent<HighscoreScript>().AddScoreValue(PlayerPrefsManager.GetScore());

        lifeControl = GameObject.FindGameObjectWithTag("MainGUI").GetComponent<SubmarineLifeControl>();
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        shooting = GameObject.Find("gun").GetComponent<PlayerShooting>();
        powerUpSpawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
    }

    void Update()
    {
        if (!paused)
        {
        timeElapsed += Time.deltaTime;
        timeUntilLevelEnd -= Time.deltaTime;
        }
        countDown.text = ((int)timeUntilLevelEnd / 60).ToString() + ":" + ((int)timeUntilLevelEnd % 60).ToString();

        if (timeUntilLevelEnd <= 0 && lifeControl.GetLifes() > 0)
        {
            SaveAndFinishLevel();
        }

        if (!CheckGazeDataAvailable() && blockWhenNoGazeData)
        {
            noGazeDataTimer += Time.deltaTime;
        }
        else
        {
            noGazeDataTimer = 0;
        }

        if (noGazeDataTimer > maxNoGazeDataTime)
        {
            noGazeDataText.SetActive(true);
            PauseGame();
        }

        if (Input.GetButtonDown("Escape"))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    private void SaveAndFinishLevel()
    {
        PlayerPrefsManager.SetCurrentLive(lifeControl.GetLifes());
        PlayerPrefsManager.SetScore(score);
        PlayerPrefsManager.SetLevel(theNextLevel);
        shooting.SaveAmmo();

        Application.LoadLevel(sceneWhenFinished);
    }

    public void PauseGame()
    {
        Screen.showCursor = true;
        pausePanel.SetActive(true);
        paused = true;
        movePlayer.stopPlayerMovement();
        shooting.BlockShooting();
        StopEnemies();
        StopPowerUps();
        noGazeDataTimer = 0;
    }

    public void UnpauseGame()
    {
        noGazeDataText.SetActive(false);
        Screen.showCursor = false;
        pausePanel.SetActive(false);
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
            controller.StartEnemyMovement();
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
        if (Input.GetButton("1"))
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
