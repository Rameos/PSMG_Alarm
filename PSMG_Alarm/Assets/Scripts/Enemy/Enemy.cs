using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private Vector3 targetLocation;

    public GameObject camera2d;
    public GameObject explosion;
    public float speed = 2;

    private HighscoreScript highscorecontroller;
    private SubmarineLifeControl submarineLifeControl;
    private bool moveAllowed = true;

    void Start()
    {
        camera2d = GameObject.Find("2D Camera");
        highscorecontroller = GameObject.FindObjectOfType(typeof(HighscoreScript)) as HighscoreScript;
        submarineLifeControl = GameObject.FindObjectOfType(typeof(SubmarineLifeControl)) as SubmarineLifeControl;

        GetNewTargetLocation();
        FindOtherObjects();
    }

    void Update()
    {
        Shoot();
        Move();
    }

    public virtual void FindOtherObjects()
    {
        // Override if necessary
    }

    public virtual void Shoot()
    {
        // Override if shooting necessary
    }

    public virtual void Move()
    {
        if (moveAllowed && Network.isServer && NetworkManagerScript.networkActive || moveAllowed && NetworkManagerScript.networkActive == false)
        {
            TurnToTarget();

            transform.position = transform.position + transform.right * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetLocation) < 1)
            {
                GetNewTargetLocation();
            }
        }
    }

    void GetNewTargetLocation()
    {
        float screenX = Random.Range(0.0f, camera2d.camera.pixelWidth);
        float screenY = Random.Range(0.0f, camera2d.camera.pixelHeight);
        targetLocation = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        targetLocation.z = 9;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            highscorecontroller.addScoreValue(100);
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Player" && networkView.isMine && NetworkManagerScript.networkActive || col.gameObject.tag == "Player" && NetworkManagerScript.networkActive == false)
        {
            highscorecontroller.addScoreValue(100);
            submarineLifeControl.decrementLife();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Shield")
        {
            Destroy(GameObject.Find("Shield(Clone)"));
            highscorecontroller.addScoreValue(100);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Wave")
        {
            highscorecontroller.addScoreValue(100);
            Destroy(gameObject);
        }
    }

    void TurnToTarget()
    {
        float angle = Vector3.Angle(transform.position - targetLocation, -transform.right);
        if (angle > 20)
        {
            transform.Rotate(Vector3.forward, 10);
        }
    }

    public void stopEnemyMovement()
    {
        moveAllowed = false;
    }

    public void startEnemyMovement()
    {
        moveAllowed = true;
    }

    public bool getMoveAllowed()
    {
        return moveAllowed;
    }

    public void setSpeed(float value)
    {
        speed = value;
    }
}
