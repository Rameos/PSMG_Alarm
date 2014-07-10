using UnityEngine;
using System.Collections;

public class enemyRocketHit : MonoBehaviour {


    private SubmarineLifeControl submarineLifeControl;
	// Use this for initialization
	void Start () {
        submarineLifeControl = GameObject.FindObjectOfType(typeof(SubmarineLifeControl)) as SubmarineLifeControl;
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
