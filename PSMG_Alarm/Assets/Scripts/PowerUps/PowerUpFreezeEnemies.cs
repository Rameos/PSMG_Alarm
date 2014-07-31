using UnityEngine;
using System.Collections;

public class PowerUpFreezeEnemies : PowerUp {

    public override void ApplyPowerUp()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().stopEnemyMovement();
        }
    }

}
