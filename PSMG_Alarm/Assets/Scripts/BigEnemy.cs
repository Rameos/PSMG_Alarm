using UnityEngine;
using System.Collections;

public class BigEnemy : Enemy
{
    public GameObject enemyBullet;
    public float minBulletTimer;

    private GameObject submarine;
    private float bulletTimer;

    public override void Shoot()
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer > minBulletTimer && Random.Range(0, 300) == 1 && getMoveAllowed())
        {
            Vector3 target = submarine.transform.position;
            float angleEnemy = Mathf.Atan2(submarine.transform.position.y, submarine.transform.position.x) * Mathf.Rad2Deg - 90;
            GameObject bullet = (GameObject)Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleEnemy + 90));

            bullet.rigidbody2D.AddForce(bullet.transform.right * 300);
            Destroy(bullet, 3);
            bulletTimer = 0;
        }
    }

    public override void FindOtherObjects()
    {
        submarine = GameObject.FindGameObjectWithTag("Player");
    }
}
