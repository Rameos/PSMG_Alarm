using UnityEngine;
using System.Collections;

public class PowerUpMovement : MonoBehaviour
{

    private Vector3 targetLocation;
    public GameObject camera2d;
	public PowerUpEffectScript powerUpEffectScript;
    private PowerUpSpawner spawner;
    public float speed;

    // Use this for initialization
    void Start()
    {
        speed = 0.2f;
        camera2d = GameObject.Find("2D Camera");
        spawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
		powerUpEffectScript = GameObject.FindObjectOfType(typeof(PowerUpEffectScript)) as PowerUpEffectScript;
        GetNewTargetLocation();
    }

    // Update is called once per frame
    void Update()
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
        if (col.gameObject.tag == "Player")
        {
			powerUpEffectScript.shield();
            Destroy(gameObject);
            spawner.removePowerUp();

        }
    }
}
