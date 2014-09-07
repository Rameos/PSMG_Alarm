using UnityEngine;
using System.Collections;

public class EggEnemy : Enemy
{

    private int enemyCount;
    private int startPopulation;
    private float maxSurviveTime = 15;
    private float surviveTime;
    private float populateTimer;
    private GameObject enemyType;
    private Vector3 target;
    private bool isOpen;
    private bool spawned;
    private bool isFading;

    public override void FindOtherObjects()
    {
        isOpen = false;
        spawned = false;
        surviveTime = maxSurviveTime;
        populateTimer = maxSurviveTime;
        target = GetNewTargetLocation();
    }


    public override void Move()
    {
        surviveTime -= Time.deltaTime;
        populateTimer -= Time.deltaTime;

        if (surviveTime <= 0f && !isOpen)
        {
            isOpen = true;
            enemyCount = 2 * startPopulation - Mathf.RoundToInt(startPopulation * (populateTimer / maxSurviveTime));
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
            isFading = true;

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

        if (isFading)
        {
            float alpha = GetComponent<SpriteRenderer>().color.a;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha -= Time.deltaTime * 2);
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
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "XRay" && !isOpen)
        {
            surviveTime -= Time.deltaTime * 3;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void SetWhite()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Populate(int count)
    {
        enemyCount = count;
        startPopulation = count;
    }

    public void GiveType(GameObject type)
    {
        enemyType = type;
    }
}
