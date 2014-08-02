using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Vector3 targetLocation;

    public GameObject camera2d;
    public GameObject explosion;
    public GameObject spark;

    public float speed = 2;
    public int life = 100;
    public int value = 100;

    private HighscoreScript highscorecontroller;
    private SubmarineLifeControl submarineLifeControl;
    private bool moveAllowed = true;

    void Start()
    {
        camera2d = GameObject.Find("2D Camera");
        highscorecontroller = GameObject.FindObjectOfType(typeof(HighscoreScript)) as HighscoreScript;
        submarineLifeControl = GameObject.FindObjectOfType(typeof(SubmarineLifeControl)) as SubmarineLifeControl;

        targetLocation = GetNewTargetLocation();
        FindOtherObjects();
    }

    void Update()
    {
        Shoot();

        if (moveAllowed)
        {
            Move();
        }

        if (life <= 0)
        {
            DestroyEnemy();
        }
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
        TurnToTarget();

        transform.position = transform.position + transform.right * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetLocation) < 1)
        {
            targetLocation = GetNewTargetLocation();
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public Vector3 GetNewTargetLocation()
    {
        float screenX = Random.Range(0.0f, camera2d.camera.pixelWidth);
        float screenY = Random.Range(0.0f, camera2d.camera.pixelHeight);
        Vector3 target = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        target.z = 9;

        return target;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MachineGun")
        {
            highscorecontroller.addScoreValue(value);
            Instantiate(spark, transform.position, transform.rotation);
            col.gameObject.SendMessage("DoDamage", this.gameObject);
        }
        if (col.gameObject.tag == "Rocket")
        {
            highscorecontroller.addScoreValue(value);
            Instantiate(explosion, transform.position, transform.rotation);
            col.gameObject.SendMessage("DoDamage", this.gameObject);
        }
        else if (col.gameObject.tag == "Player" && networkView.isMine && NetworkManagerScript.networkActive || col.gameObject.tag == "Player" && NetworkManagerScript.networkActive == false)
        {
            highscorecontroller.addScoreValue(value);
            submarineLifeControl.decrementLife();
            DestroyEnemy();
        }
        else if (col.gameObject.tag == "Shield")
        {
            Destroy(GameObject.Find("Shield(Clone)"));
            highscorecontroller.addScoreValue(100);
            DestroyEnemy();
        }
        else if (col.gameObject.tag == "Wave")
        {
            highscorecontroller.addScoreValue(100);
            col.gameObject.SendMessage("DoDamage", this.gameObject);
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

    public void TakeDamage(int value)
    {
        life -= value;
    }
}
