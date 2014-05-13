using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    public Rigidbody2D rocket;
    public float speed = 15f;

    private MovePlayer uboot;
    
	// Use this for initialization
	void Awake () {
        uboot = transform.root.GetComponent<MovePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(speed, 0);

        }
	}
}
