using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    public GameObject[] rocket;
    public float speed = 19f;
    

    private MovePlayer uboot;
    
	// Use this for initialization
	void Awake () {
        uboot = transform.root.GetComponent<MovePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}
    void FixedUpdate()
    {
        Vector3 aimPositon = Input.mousePosition;
        aimPositon.z = 0.0f;
        //Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        aimPositon.x = aimPositon.x - ubootposition.x;
        aimPositon.y = aimPositon.y - ubootposition.y;
        float angle = Mathf.Atan2(aimPositon.y, aimPositon.x) * Mathf.Rad2Deg - 90;
        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);
        

        if (Input.GetButtonDown("Fire1"))
        {
            int ammoIndex = 0;
            //Debug.Log("posx" + aimPositon.x + " posy " + aimPositon.y +"angle"+ angle);
            GameObject bulletInstance = (GameObject)Instantiate(rocket[ammoIndex], transform.position,transform.rotation);

            
            //bulletInstance.velocity = new Vector2(aimPositon.x / speed, aimPositon.y / speed);
            Debug.Log("mouseposition" + aimPositon);
            bulletInstance.transform.LookAt(aimPositon);
           // bulletInstance.rigidbody2D.AddForce(bulletInstance.transform.forward * speed);

        }
    }
}
