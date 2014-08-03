using UnityEngine;
using System.Collections;

public class GhostEnemy : Enemy
{

    private Vector3 target;
    private bool invisible;
    private float invTimer;

    public override void FindOtherObjects()
    {
        target = GetNewTargetLocation();
        invTimer = Random.Range(5f, 10f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!invisible)
        {
            if (col.gameObject.tag == "MachineGun")
            {
                Instantiate(spark, transform.position, transform.rotation);
                col.gameObject.SendMessage("DoDamage", this.gameObject);
            }
            else if (col.gameObject.tag == "Rocket")
            {
                Instantiate(explosion, transform.position, transform.rotation);
                col.gameObject.SendMessage("DoDamage", this.gameObject);
            }
            else if (col.gameObject.tag == "Wave")
            {
                col.gameObject.SendMessage("DoDamage", this.gameObject);
            }
        }
        if (col.gameObject.tag == "Player" && networkView.isMine && NetworkManagerScript.networkActive || col.gameObject.tag == "Player" && NetworkManagerScript.networkActive == false)
        {
            submarineLifeControl.DecrementLife();
            DestroyEnemy();
        }
        else if (col.gameObject.tag == "Shield")
        {
            player.SendMessage("DestroyShield");
            Destroy(GameObject.Find("Shield(Clone)"));
            DestroyEnemy();
        }
    }

    public override void Move()
    {
        invTimer -= Time.deltaTime;
        transform.position = transform.position + ((target - transform.position).normalized) * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < 1)
        {
            target = GetNewTargetLocation();
        }

        ChangeInvisibility();
    }

    void ChangeInvisibility()
    {
        if (invTimer <= 0)
        {
            invTimer = Random.Range(5f, 10f);
            invisible = !invisible;
        }

        if (invisible && gameObject.renderer.material.color.a > 0.3)
        {
            gameObject.renderer.material.color = new Color(gameObject.renderer.material.color.r, gameObject.renderer.material.color.g,
                gameObject.renderer.material.color.b, gameObject.renderer.material.color.a - 0.1f);
        }
        else if (!invisible && gameObject.renderer.material.color.a < 1)
        {
            gameObject.renderer.material.color = new Color(gameObject.renderer.material.color.r, gameObject.renderer.material.color.g,
                gameObject.renderer.material.color.b, gameObject.renderer.material.color.a + 0.1f);
        }
    }

}
