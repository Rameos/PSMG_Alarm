using UnityEngine;
using System.Collections;
using System.Threading;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject[] powerUps;
    public GameObject camera2d;
    private GameOverScript gameOver;
    private int updateCount;
    private int randomNumber;
    private int powerUpsCount;
    private const int MAX_POWERUPS_COUNT = 1;
    private bool powerUpsCanSpawn = true;

    void Start()
    {
        updateCount = 0;
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
    }

    void Update()
    {
        updateCount++;
        randomNumber = Random.Range(100, 200);
        if (updateCount % randomNumber == 0 && powerUpsCount < MAX_POWERUPS_COUNT && !gameOver.GetGameOver() && powerUpsCanSpawn)
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

        Instantiate(powerUps[Random.Range(0, powerUps.Length)], pos, Quaternion.Euler(0, 0, 0));

        powerUpsCount++;
    }

    public void RemovePowerUp()
    {
        powerUpsCount--;
        updateCount = 0;
    }

    public void StopSpawning()
    {
        powerUpsCanSpawn = false;
    }

    public void StartSpawning()
    {
        powerUpsCanSpawn = true;
    }
}

