using UnityEngine;
using System.Collections;

public class PowerUpSlowEnemyEffectScript : MonoBehaviour {

	private EnemyMovement enemyMovement;
	private ShootingEnemyMovement shootingEnemyMovement;
	private GameObject[] enemies, redEnemies;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void slowEnemies() {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		redEnemies = GameObject.FindGameObjectsWithTag("RocketEnemy");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemyMovement = enemies[i].GetComponent<EnemyMovement>();
			enemyMovement.stopEnemyMovement(); 
		}

		for (int i = 0; i < redEnemies.Length; i++)
		{
			shootingEnemyMovement = redEnemies[i].GetComponent<ShootingEnemyMovement>();
			enemyMovement.stopEnemyMovement(); 
		}
	}
}
