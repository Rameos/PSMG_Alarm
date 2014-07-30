using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject enemyRed;
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
		float screenX;
		float screenY;
		
		int edge = Random.Range(0, 4);
		
		if (edge == 0)
		{
			screenX = Random.Range(camera2d.camera.pixelWidth, camera2d.camera.pixelWidth + 2);
			screenY = Random.Range(0, -1);
		} 
		else if (edge == 1)
		{
			screenX = Random.Range(0, -2);
			screenY = Random.Range(camera2d.camera.pixelHeight, camera2d.camera.pixelHeight + 2);
		} 
		else if (edge == 2)
		{
			screenX = Random.Range(0, -2);
			screenY = Random.Range(0, -2);
		} 
		else
		{
			screenX = Random.Range(camera2d.camera.pixelWidth, camera2d.camera.pixelWidth + 2);
			screenY = Random.Range(camera2d.camera.pixelHeight, camera2d.camera.pixelHeight + 2);
		}
        Vector3 randomPos = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        randomPos.z = 9;

        return randomPos;
    }

    public void SpawnEnemy()
    {
        int random= Random.Range(0, 2);
        Vector3 pos = GetRandomPosition();
        
		if (NetworkManagerScript.networkActive && Network.isServer) {
			if (random == 0)
			{
				Network.Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0),5);
			}
			else if (random == 1)
			{
				Network.Instantiate(enemyRed, pos, Quaternion.Euler(0, 0, 0),6);
			}
		}
		if (NetworkManagerScript.networkActive == false) {
			if (random == 0)
			{
				Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0));
			}
			else if (random == 1)
			{
				Instantiate(enemyRed, pos, Quaternion.Euler(0, 0, 0));
			}	
		}
        enemiesAlive++;
    }

    private void EnemyDied()
    {
        enemiesAlive--;
    }
}