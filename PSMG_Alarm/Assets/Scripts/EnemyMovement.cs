using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private Vector3 targetLocation;

    public GameObject camera2d;

    public float speed = 2;

	void Start () 
    {
        camera2d = GameObject.Find("2D Camera");
        GetNewTargetLocation();
	}
	

	void Update () 
    {
        MoveToTargetLocation();
	}

    void GetNewTargetLocation()
    {
        float screenX = Random.Range(0.0f, camera2d.camera.pixelWidth);
        float screenY = Random.Range(0.0f, camera2d.camera.pixelHeight);
        targetLocation = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        targetLocation.z = 9;
    }

    void MoveToTargetLocation()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
        if (transform.position == targetLocation)
            GetNewTargetLocation();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
