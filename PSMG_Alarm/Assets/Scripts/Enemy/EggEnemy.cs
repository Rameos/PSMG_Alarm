using UnityEngine;
using System.Collections;

public class EggEnemy : Enemy {

    private int enemyCount;
    private GameObject enemyType;
    private Vector3 target;

    public override void FindOtherObjects()
    {
        target = GetNewTargetLocation();
    }


	public override void Move()
    {
        transform.Rotate(0, 0, 0.5f);

        transform.position = transform.position + ((target - transform.position).normalized) * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < 1)
        {
            target = GetNewTargetLocation();
        }
    }

    public void Populate(int count)
    {
        enemyCount = count;
    }

    public void GiveType(GameObject type)
    {
        enemyType = type;
    }
}
