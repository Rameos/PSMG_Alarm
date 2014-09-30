using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Endbossspawn : MonoBehaviour
{
	public GameObject camera2d;
	public GameObject minePrefab;
	
	private GameOverScript gameOver;
	private float spawnTimer;
	private float time = 0f;

	void Start()
	{
		spawnTimer = Random.Range(1.0f, 1.1f);
		gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
	}
	
	void Update()
	{
		if (gameOver.GetGameOver())
		{
			Destroy(this);
		}
		time += Time.deltaTime;
		if (time > spawnTimer) 
		{
			time = 0;
			spawnTimer = Random.Range(1.0f, 5f);
			SpawnEnemy(minePrefab);
		}
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
	
	public void SpawnEnemy(GameObject type)
	{
		Vector3 pos = GetRandomPosition();
		
		NetworkManagerScript.NetworkInstantiate(type, pos, Quaternion.Euler(0, 0, 0));
	}

}