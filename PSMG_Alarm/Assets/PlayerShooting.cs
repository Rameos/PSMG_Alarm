using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] rocket;
    public float speed = 19f;


    private MovePlayer uboot;

    // Use this for initialization
    void Awake()
    {
        uboot = transform.root.GetComponent<MovePlayer>();
    }

    // Update is called once per frame

    void Update()
    {
        Vector3 aimPositon = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
         //  Vector3 aimPositon = Input.mousePosition;
        aimPositon.z = 0.0f;
        //Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        aimPositon.x = aimPositon.x - ubootposition.x;
        aimPositon.y = aimPositon.y - ubootposition.y;
        float angle = Mathf.Atan2(-aimPositon.y, aimPositon.x) * Mathf.Rad2Deg - 90;
        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);


        if (Input.GetButtonDown("Fire1"))
        {
            int ammoIndex = 0;
            GameObject bulletInstance = (GameObject)Instantiate(rocket[ammoIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            bulletInstance.rigidbody2D.AddForce(bulletInstance.transform.right * 1000);
            Debug.Log("bulletForce: " + bulletInstance.transform.right + "bulletForce forward" + bulletInstance.transform.forward);
            Destroy(bulletInstance, 2);
        }

    }
}
