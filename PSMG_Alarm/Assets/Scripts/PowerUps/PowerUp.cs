﻿using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{

    private Vector3 targetLocation;
    private GameObject camera2d;
    private PowerUpSpawner spawner;
    public float speed = .2f;
    private bool moveAllowed = true;

    void Start()
    {
        camera2d = GameObject.Find("2D Camera");
        spawner = GameObject.Find("GameController").GetComponent<PowerUpSpawner>();
        
        GetNewTargetLocation();
        FindOtherObjects();
    }

    void Update()
    {
        Move();
    }

    public virtual void FindOtherObjects()
    {
        // Override if necessary
    }

    public virtual void Move()
    {
        if (moveAllowed) MoveToTargetLocation();
    }

    public virtual void ApplyPowerUp()
    {
        // override this
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ApplyPowerUp();

            Destroy(gameObject);
            spawner.removePowerUp();
        }
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

    public void stopPowerUpMovement()
    {
        moveAllowed = false;
    }
    public void startPowerUpMovement()
    {
        moveAllowed = true;
    }

    public bool getMoveAllowed()
    {
        return moveAllowed;
    }
}
