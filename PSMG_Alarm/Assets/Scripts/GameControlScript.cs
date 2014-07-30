using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {
	
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
		powerUpSpawner = GameObject.Find ("GameController").GetComponent<PowerUpSpawner> ();
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
	
	void stopEnemies()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
	}
	
	void stopPowerUps()
	{
		powerUpSpawner.stopSpawning();
		
		PowerUpMovement powerUpMovement;
		for (int i = 0; i < powerUps.Length; i++)
		{
			powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>();
			powerUpMovement.stopPowerUpMovement();
		}
	}
	
	void startEnemies()
	{
        Enemy[] enemies = GetComponents<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].stopEnemyMovement();
        }
	}
	
	void startPowerUps()
	{
		powerUpSpawner.startSpawning();
		
		PowerUpMovement powerUpMovement;
		for (int i = 0; i < powerUps.Length; i++)
		{
			powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>();
			powerUpMovement.startPowerUpMovement();
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
