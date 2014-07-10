using UnityEngine;
using System.Collections;

public class PowerUpMovement : MonoBehaviour
{

    private Vector3 targetLocation;
    public GameObject camera2d;
	public PowerUpEffectScript powerUpEffectScript;
	public PowerUpSlowEnemyEffectScript powerUpSlowEnemyEffectScript; 
    private PowerUpSpawner spawner;
    public float speed;
    private bool moveAllowed = true;

    // Use this for initialization
    void Start()
    {
        speed = 0.2f;
        camera2d = GameObject.Find("2D Camera");
        spawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
		powerUpEffectScript = GameObject.FindObjectOfType(typeof(PowerUpEffectScript)) as PowerUpEffectScript;
		powerUpSlowEnemyEffectScript = GameObject.FindObjectOfType (typeof(PowerUpSlowEnemyEffectScript)) as PowerUpSlowEnemyEffectScript;
		GetNewTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveAllowed) MoveToTargetLocation();
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
		Debug.Log (this.gameObject.name); 
		Debug.Log (col.gameObject.tag); 
        if (col.gameObject.tag == "Player")
        {
			Debug.Log (this.gameObject.name); 
			if(this.gameObject.name == "SlowEnemyPowerUp(Clone)") {
				powerUpSlowEnemyEffectScript.slowEnemies();
				Debug.Log ("slow"); 
			}
			if(this.gameObject.name == "PowerUp(Clone)") {
				powerUpEffectScript.shield();
				Debug.Log("fast"); 
			} else {
				Debug.Log ("else"); 
			}
			Destroy(gameObject);
			spawner.removePowerUp();

        }
    }

    public void stopPowerUpMovement()
    {
        moveAllowed = false;
    }

    public bool getMoveAllowed()
    {
        return moveAllowed;
    }
}
