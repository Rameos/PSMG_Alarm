using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject camera2d;
    private GameOverScript gameOver; 
   
    private int enemiesAlive;
    public int enemiesSouldBeAlive;

    void Start()
    {
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
    }

    void Update()
    {
        if (enemiesAlive < enemiesSouldBeAlive && !gameOver.getGameOver())
            SpawnEnemy();
    }

    private Vector3 GetRandomPosition()
    {
        float screenX = Random.Range(0.0f, camera2d.camera.pixelWidth);
        float screenY = Random.Range(0.0f, camera2d.camera.pixelHeight);
        Vector3 randomPos = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        randomPos.z = 9;

        return randomPos;
    }

    public void SpawnEnemy()
    {
        Vector3 pos = GetRandomPosition();
        GameObject clone = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        enemiesAlive++;
    }

    private void EnemyDied()
    {
        enemiesAlive--;
    }
}