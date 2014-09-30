using UnityEngine;
using System.Collections;

public class PowerUpFreezeEnemies : PowerUp
{
    public override void ApplyPowerUp()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position, 0.6f);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().SlowEnemy();
        }
    }
}