using UnityEngine;
using System.Collections;
using System.Threading;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerUp;
	public GameObject slowEnemyPowerUp;
    public GameObject camera2d;
    private GameOverScript gameOver; 
    private int updateCount;
    private int randomNumber;
	private int randomPowerUpNumber; 
    private int powerUpsCount;
    private const int MAX_POWERUPS_COUNT = 3;
	private bool powerUpsCanSpawn = true;

    // Use this for initialization
    void Start()
    {
        updateCount = 0;
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        updateCount++;
        randomNumber = Random.Range(100, 200);
        if (updateCount % randomNumber == 0 && powerUpsCount < MAX_POWERUPS_COUNT && !gameOver.getGameOver() && powerUpsCanSpawn)
        {
            SpawnPowerup();
            updateCount = 0;
        }

    }

    private Vector3 GetRandomPosition()
    {
        float screenX = Random.Range(0.0f, camera2d.camera.pixelWidth);
        float screenY = Random.Range(0.0f, camera2d.camera.pixelHeight);
        Vector3 randomPos = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        randomPos.z = 9;

        return randomPos;
    }

    public void SpawnPowerup()
    {
        Vector3 pos = GetRandomPosition();
		randomPowerUpNumber = Random.Range (1, 3); 
		GameObject clone = randomPowerUpNumber == 1 ? (Instantiate(powerUp, pos, Quaternion.Euler(0, 0, 0)) as GameObject) : (Instantiate(slowEnemyPowerUp, pos, Quaternion.Euler(0, 0, 0)) as GameObject);
        powerUpsCount++;
    }

    public void removePowerUp()
    {
        powerUpsCount--;
    }

	public void stopSpawning()
	{
		powerUpsCanSpawn = false;
	}
	
	public void startSpawning()
	{
		powerUpsCanSpawn = true;
	}
}

