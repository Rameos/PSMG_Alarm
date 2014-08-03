using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineTrigger : MonoBehaviour {

    private bool explosionTriggered;
    private float timer = 1f;
    List<GameObject> insideObject = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D col)
    {
        insideObject.Add(col.gameObject);

        if (col.gameObject.tag == "Player")
        {
            explosionTriggered = true;
            this.gameObject.GetComponent<Animator>().SetTrigger("Triggered");
        }
    }

    void Update()
    {
        if (explosionTriggered)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            this.gameObject.transform.parent.gameObject.SendMessage("DestroyEnemy");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        insideObject.Remove(col.gameObject);
    }

    public void TriggerNow()
    {
        foreach(GameObject ga in insideObject)
        {
            if (ga.tag == "Enemy")
            {
                ga.SendMessage("TakeDamage", 100);
            }

            if (ga.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("MainGUI").GetComponent<SubmarineLifeControl>().DecrementLife();
            }
        }
    }
}
