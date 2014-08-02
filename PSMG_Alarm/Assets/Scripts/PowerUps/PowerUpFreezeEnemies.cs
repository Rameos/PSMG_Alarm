using UnityEngine;
using System.Collections;

public class PowerUpFreezeEnemies : PowerUp
{
    public override void ApplyPowerUp()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().stopEnemyMovement();
            enemy.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
        }
    }
}