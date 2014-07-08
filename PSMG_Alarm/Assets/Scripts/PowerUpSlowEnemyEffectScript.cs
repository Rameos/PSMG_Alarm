using UnityEngine;
using System.Collections;

public class PowerUpSlowEnemyEffectScript : MonoBehaviour {

	private EnemyMovement enemyMovement; 
	private GameObject[] enemies;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void slowEnemies() {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemyMovement = enemies[i].GetComponent<EnemyMovement>();
			enemyMovement.stopEnemyMovement(); 
		}
	}
}
