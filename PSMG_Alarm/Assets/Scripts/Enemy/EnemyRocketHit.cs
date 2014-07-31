using UnityEngine;
using System.Collections;

public class EnemyRocketHit : MonoBehaviour
{
    private SubmarineLifeControl submarineLifeControl;

    void Start()
    {
        submarineLifeControl = GameObject.FindObjectOfType(typeof(SubmarineLifeControl)) as SubmarineLifeControl;

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            submarineLifeControl.decrementLife();
        }

        else if (col.gameObject.tag == "Shield")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

    }
}
