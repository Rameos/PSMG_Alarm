using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour
{

    public float maxNoGazeDataTime = 1;
    public bool blockWhenNoGazeData = true;

    private GameObject[] powerUps;
    private PowerUpSpawner powerUpSpawner;
    private PlayerShooting shooting;
    private MovePlayer movePlayer;
    private float noGazeDataTimer;
    private bool paused = false;

    void Start()
    {
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        shooting = GameObject.Find("gun").GetComponent<PlayerShooting>();
        powerUpSpawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
    }

    void Update()
    {
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

        PowerUpMovement powerUpMovement;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>();
            powerUpMovement.stopPowerUpMovement();
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
        powerUpSpawner.startSpawning();

        PowerUpMovement powerUpMovement;
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>();
            powerUpMovement.startPowerUpMovement();
        }
    }

    public void stopParticles()
    {
        GameObject[] particleEmitter = GameObject.FindGameObjectsWithTag("MovementParticles");

        for (int i = 0; i < particleEmitter.Length; i++)
        {
            particleEmitter[i].GetComponent<ParticleSystem>().Stop();
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
}
