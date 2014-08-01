using UnityEngine;
using System.Collections;

public class EnemySeamine : Enemy
{
    private Vector3 target;

    public override void Move()
    {
        transform.Rotate(0, 0, 2);

        transform.position = transform.position + ((target - transform.position).normalized) * speed * Time.deltaTime;
        
        if (Vector3.Distance(transform.position, target) < 1)
        {
           target = GetNewTargetLocation();
        }
    }

}