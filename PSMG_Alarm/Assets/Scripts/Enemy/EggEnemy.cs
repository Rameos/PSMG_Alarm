using UnityEngine;
using System.Collections;

public class EggEnemy : Enemy
{

    private int enemyCount;
    private float surviveTime = 10;
    private GameObject enemyType;
    private Vector3 target;
    private bool isOpen;
    private bool spawned;

    public override void FindOtherObjects()
    {
        isOpen = false;
        spawned = false;
        target = GetNewTargetLocation();
    }


    public override void Move()
    {
        surviveTime -= Time.deltaTime;

        if(surviveTime <= 0f && !isOpen)
        {
            isOpen = true;
            GetComponent<Animator>().SetTrigger("Break");
            surviveTime = 1f;
        }
        else if (surviveTime <= 0 && isOpen)
        {
            Destroy(this.gameObject);
        }

        if (surviveTime <= 0.5f && isOpen && !spawned)
        {
            Destroy(GetComponent<CircleCollider2D>());

            for (int i = 0; i < enemyCount; i++)
            {
                NetworkManagerScript.NetworkInstantiate(enemyType, transform.position, Quaternion.Euler(0, 0, 0));
            }

            spawned = true;
        }

        transform.Rotate(0, 0, 0.5f);

        transform.position = transform.position + ((target - transform.position).normalized) * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < 1)
        {
            target = GetNewTargetLocation();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            foreach (ContactPoint2D contacts in col.contacts)
            {
                submarineLifeControl.DecrementLife();
                col.gameObject.rigidbody2D.AddForce(contacts.normal * 2000);
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            submarineLifeControl.DecrementLife();
            DestroyEnemy();
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
