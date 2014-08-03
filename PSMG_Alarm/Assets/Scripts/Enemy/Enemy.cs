using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Vector3 targetLocation;

    public GameObject explosion;
    public GameObject spark;
    public GameObject drop;

    public float speed = 2;
    public int life = 100;
    public int value = 100;

    private GameObject camera2d;
    private HighscoreScript highscorecontroller;
    private SubmarineLifeControl submarineLifeControl;
    private GameObject player;
    private bool moveAllowed = true;
    private bool slowed = false;

    void Start()
    {
        camera2d = GameObject.Find("2D Camera");
        player = GameObject.FindGameObjectWithTag("Player");
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

    public virtual void DestroyEnemy()
    {
        highscorecontroller.AddScoreValue(value);
        Instantiate(explosion, transform.position, transform.rotation);

        if (Random.Range(0, 4) == 0 && drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.Euler(Vector3.zero));
        }

        Destroy(gameObject);
    }

    public Vector3 GetNewTargetLocation()
    {
        float screenX = Random.Range(20, camera2d.camera.pixelWidth - 20);
        float screenY = Random.Range(20, camera2d.camera.pixelHeight - 20);
        Vector3 target = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        target.z = 9;

        return target;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MachineGun")
        {
            Instantiate(spark, transform.position, transform.rotation);
            col.gameObject.SendMessage("DoDamage", this.gameObject);
        }
        if (col.gameObject.tag == "Rocket")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            col.gameObject.SendMessage("DoDamage", this.gameObject);
        }
        else if (col.gameObject.tag == "Player" && networkView.isMine && NetworkManagerScript.networkActive || col.gameObject.tag == "Player" && NetworkManagerScript.networkActive == false)
        {
            submarineLifeControl.DecrementLife();
            DestroyEnemy();
        }
        else if (col.gameObject.tag == "Shield")
        {
            player.SendMessage("DestroyShield");
            Destroy(GameObject.Find("Shield(Clone)"));
            DestroyEnemy();
        }
        else if (col.gameObject.tag == "Wave")
        {
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

    public void StopEnemyMovement()
    {
        moveAllowed = false;
    }

    public void SlowEnemy()
    {
        if (!slowed)
        {
            SetSpeed(speed * 0.3f);
        }
    }

    public void StartEnemyMovement()
    {
        moveAllowed = true;
    }

    public bool GetMoveAllowed()
    {
        return moveAllowed;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void TakeDamage(int value)
    {
        life -= value;
    }
}
