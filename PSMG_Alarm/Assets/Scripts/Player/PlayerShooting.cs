﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour
{
    public WeaponObject[] weapon;
    public bool useGazeControl = false;

    private GameOverScript gameOver;
    private GameObject crosshair;

    public enum weaponTyps { rocket, laser, mg };
    public weaponTyps weaponTyp = weaponTyps.rocket;

    private float fireTimer;
    private bool shootingBlocked;

    void Start()
    {
        fireTimer = weapon[0].fireRate;
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
        crosshair = GameObject.Find("Crosshair");
    }

    void Update()
    {
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 aimPosition;

        fireTimer += Time.deltaTime;

        if (useGazeControl)
        {
            aimPosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            aimPosition.y = (Screen.height - aimPosition.y) - ubootposition.y;
        }
        else
        {
            aimPosition = Input.mousePosition;
            aimPosition.y = aimPosition.y - ubootposition.y;
        }

        crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        crosshair.transform.position = new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, 0);
        aimPosition.z = 0.0f;
        aimPosition.x = aimPosition.x - ubootposition.x;

        float angle = Mathf.Atan2(aimPosition.y, aimPosition.x) * Mathf.Rad2Deg - 90;

        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);

        if (Input.GetButtonDown("Machine Gun"))
        {
            weaponTyp = weaponTyps.rocket;
            fireTimer = weapon[(int)weaponTyp].fireRate;
        }
        else if (Input.GetButtonDown("Rocket"))
        {
            weaponTyp = weaponTyps.laser;
            fireTimer = weapon[(int)weaponTyp].fireRate;
        }
        else if (Input.GetButtonDown("Laser"))
        {
            weaponTyp = weaponTyps.mg;
            fireTimer = weapon[(int)weaponTyp].fireRate;
        }

        if (fireTimer >= weapon[(int)weaponTyp].fireRate)
        {
            if (Input.GetButton("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive && networkView.isMine && !shootingBlocked ||
                Input.GetButton("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive == false && !shootingBlocked)
            {
                GameObject bulletInstance;

                if (NetworkManagerScript.networkActive && networkView.isMine)
                {
                    bulletInstance = (GameObject)Network.Instantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation, (int)weaponTyp);
                }
                else
                {
                    bulletInstance = (GameObject)Instantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation);
                }

                bulletInstance.SendMessage("AssignDamage", weapon[(int)weaponTyp].damage);
                bulletInstance.SendMessage("Fire", aimPosition);
                fireTimer = 0;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive && networkView.isMine && !shootingBlocked ||
                Input.GetButtonDown("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive == false && !shootingBlocked)
            {
                GameObject bulletInstance;

                if (NetworkManagerScript.networkActive && networkView.isMine)
                {
                    bulletInstance = (GameObject)Network.Instantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation, (int)weaponTyp);
                }
                else
                {
                    bulletInstance = (GameObject)Instantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation);
                }

                bulletInstance.SendMessage("AssignDamage", weapon[(int)weaponTyp].damage);
                bulletInstance.SendMessage("Fire", aimPosition);
                fireTimer = 0;
            }
        }
    }
    public void blockShooting()
    {
        shootingBlocked = true;
    }

    public void unblockShooting()
    {
        shootingBlocked = false;
    }

    [System.Serializable]
    public class WeaponObject
    {
        public GameObject weapon;
        public float fireRate;
        public int damage;
    }
}