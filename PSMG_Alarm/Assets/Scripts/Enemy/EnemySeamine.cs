using UnityEngine;
using System.Collections;

public class EnemySeamine : Enemy
{
    private Vector3 target;
    public GameObject radius;

    public override void FindOtherObjects()
    {
        target = GetNewTargetLocation();
    }

    public override void Move()
    {
        transform.Rotate(0, 0, 2);
        transform.position = transform.position + ((target - transform.position).normalized) * speed * Time.deltaTime;
        
        if (Vector3.Distance(transform.position, target) < 1)
        {
           target = GetNewTargetLocation();
        }
    }

    public override void DestroyEnemy()
    {
        radius.SendMessage("TriggerNow");
        Camera.main.GetComponent<CameraShake>().StartShaking(0.2f);
        GameObject.FindGameObjectWithTag("3DCam").GetComponent<CameraShake>().StartShaking(0.2f);

        base.DestroyEnemy();
    } 

}