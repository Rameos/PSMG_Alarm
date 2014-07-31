using UnityEngine;
using System.Collections;

public class PowerUpSlowEnemyEffectScript : MonoBehaviour {

	public void slowEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies) {
            enemy.GetComponent<Enemy>().stopEnemyMovement();
		}
	}
}
