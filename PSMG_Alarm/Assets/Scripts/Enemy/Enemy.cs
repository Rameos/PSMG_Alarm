﻿using UnityEngine;
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
    private HighscoreScript highscoreController;
    public SubmarineLifeControl submarineLifeControl;
    public GameObject player;
    private bool moveAllowed = true;
    private bool slowed = false;

    void Start()
    {
        life /= 2;
        life += PlayerPrefsManager.GetDifficulty() * life;
        camera2d = GameObject.Find("2D Camera");
        player = GameObject.FindGameObjectWithTag("Player");
        highscoreController = GameObject.FindObjectOfType(typeof(HighscoreScript)) as HighscoreScript;
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
        highscoreController.AddScoreValue(value);
        Instantiate(explosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, new Vector3(transform.position.x, transform.position.y, -10), 1);

        if (Random.Range(0, 4) == 0 && drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.Euler(Vector3.zero));
        }
        Destroy(gameObject);
    }

    public Vector3 GetNewTargetLocation()
    {
        float screenX = Random.Range(0.02f, 0.98f);
        float screenY = Random.Range(0.05f, 0.95f);
        Vector3 target = camera2d.camera.ViewportToWorldPoint(new Vector3(screenX, screenY, 9));
        target.z = 9;

        return target;
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
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
        else if (col.gameObject.tag == "Player")
        {
            DestroyEnemy();
            submarineLifeControl.DecrementLife();
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

    public virtual void SlowEnemy()
    {
        if (!slowed)
        {
            SetSpeed(speed * 0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
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
