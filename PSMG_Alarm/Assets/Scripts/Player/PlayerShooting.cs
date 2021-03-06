﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour
{
    public WeaponObject[] weapon;
    public bool useGazeControl = false;

    private GameOverScript gameOver;
    private GameObject crosshair;

    public enum weaponTyps { mg, rocket, laser };
    public weaponTyps weaponTyp = weaponTyps.rocket;

    private float fireTimer;
    private bool shootingBlocked;
    private HighscoreScript highscore;

    private int rocketAmmo;
    private int laserAmmo;
    private GameObject xRay;
    private Vector2 rawAimPosition;

    //TODO: Remove this flag
    public bool fakeEndlessAmmo;

    private int mgUpgrade = 0;

    void Start()
    {
        xRay = GameObject.FindGameObjectWithTag("XRay");
        xRay.SetActive(false);

        mgUpgrade += PlayerPrefsManager.GetUpgrade(UpgradeController.upgradeID.MACHINE_GUN_1);
        mgUpgrade += PlayerPrefsManager.GetUpgrade(UpgradeController.upgradeID.MACHINE_GUN_2);

        fireTimer = weapon[0].fireRate;
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
        crosshair = GameObject.Find("Crosshair");
        highscore = GameObject.Find("Highscore").GetComponent<HighscoreScript>();

        rocketAmmo = PlayerPrefsManager.GetUpgrade(UpgradeController.upgradeID.ROCKET_AMMO);
        laserAmmo = PlayerPrefsManager.GetUpgrade(UpgradeController.upgradeID.PHASER_AMMO);

        useGazeControl = PlayerPrefsManager.GetControl();
    }

    void Update()
    {
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 aimPosition;

        fireTimer += Time.deltaTime;

        if (useGazeControl)
        {
            rawAimPosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            rawAimPosition.y = (Screen.height - rawAimPosition.y);
            aimPosition = rawAimPosition;
            aimPosition.y = aimPosition.y - ubootposition.y;
        }
        else
        {
            aimPosition = Input.mousePosition;
            rawAimPosition = aimPosition;
            aimPosition.y = aimPosition.y - ubootposition.y;
        }

        crosshair.transform.position = Camera.main.ScreenToWorldPoint(rawAimPosition);
        crosshair.transform.position = new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, 0);
        aimPosition.x = aimPosition.x - ubootposition.x;

        float angle;
        angle = Mathf.Atan2(aimPosition.y, aimPosition.x) * Mathf.Rad2Deg - 90;

        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);

        if (Input.GetButtonDown("Machine Gun"))
        {
            weaponTyp = weaponTyps.mg;
            fireTimer = weapon[(int)weaponTyp].fireRate;
            UpdateAmmo();
        }
        else if (Input.GetButtonDown("Rocket") && rocketAmmo > 0 || Input.GetButtonDown("Rocket") && fakeEndlessAmmo)
        {
            weaponTyp = weaponTyps.rocket;
            fireTimer = weapon[(int)weaponTyp].fireRate;
            UpdateAmmo();
        }
        else if (Input.GetButtonDown("Laser") && laserAmmo > 0 || Input.GetButtonDown("Laser") && fakeEndlessAmmo)
        {
            weaponTyp = weaponTyps.laser;
            fireTimer = weapon[(int)weaponTyp].fireRate;
            UpdateAmmo();
        }

        if (fireTimer >= weapon[(int)weaponTyp].fireRate)
        {
            if (Input.GetButton("Fire1") && !gameOver.GetGameOver() && NetworkManagerScript.networkActive && networkView.isMine && !shootingBlocked ||
                Input.GetButton("Fire1") && !gameOver.GetGameOver() && NetworkManagerScript.networkActive == false && !shootingBlocked)
            {
                CheckAmmo();
                FireBullet(aimPosition);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !gameOver.GetGameOver() && !shootingBlocked)
            {
                CheckAmmo();
                FireBullet(aimPosition);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            xRay.SetActive(true);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            xRay.SetActive(false);
            GameObject[] eggs = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject egg in eggs)
            {
                if (egg.name == "EggEnemy(Clone)")
                {
                    if (egg.GetComponent<EggEnemy>() != null)
                    {
                        egg.GetComponent<EggEnemy>().SetWhite();
                    }
                }
            }

        }
    }

    private void FireBullet(Vector3 aimPosition)
    {
        GameObject bulletInstance;
        int bonusDamage = 0;

        bulletInstance = NetworkManagerScript.NetworkInstantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation, false, true);

        if (weaponTyp == weaponTyps.mg && mgUpgrade > 0)
        {
            bonusDamage = 20;

            if (mgUpgrade == 2)
            {
                GameObject bulletInstance1 = NetworkManagerScript.NetworkInstantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation * Quaternion.Euler(0, 0, 3f), false, true);
                GameObject bulletInstance2 = NetworkManagerScript.NetworkInstantiate(weapon[(int)weaponTyp].weapon, transform.position, transform.rotation * Quaternion.Euler(0, 0, -3f), false, true);

                bulletInstance1.SendMessage("AssignDamage", weapon[(int)weaponTyp].damage + bonusDamage);
                bulletInstance1.SendMessage("Fire", aimPosition);

                bulletInstance2.SendMessage("AssignDamage", weapon[(int)weaponTyp].damage + bonusDamage);
                bulletInstance2.SendMessage("Fire", aimPosition);
            }
        }



        bulletInstance.SendMessage("AssignDamage", weapon[(int)weaponTyp].damage + bonusDamage);
        bulletInstance.SendMessage("Fire", aimPosition);
        fireTimer = 0;

        UpdateAmmo();
    }

    private void CheckAmmo()
    {
        if (!fakeEndlessAmmo)
        {
            switch (weaponTyp)
            {
                case weaponTyps.rocket:
                    if (rocketAmmo <= 0)
                        weaponTyp = weaponTyps.mg;
                    else
                        rocketAmmo--;
                    break;
                case weaponTyps.laser:
                    if (laserAmmo <= 0)
                        weaponTyp = weaponTyps.mg;
                    else
                        laserAmmo--;
                    break;
            }
        }
    }

    private void UpdateAmmo()
    {
        if (!fakeEndlessAmmo)
        {
            switch (weaponTyp)
            {
                case weaponTyps.rocket:
                    highscore.UpdateAmmo(rocketAmmo);
                    break;
                case weaponTyps.laser:
                    highscore.UpdateAmmo(laserAmmo);
                    break;
                case weaponTyps.mg:
                    highscore.UpdateAmmo(-1);
                    break;
            }
        }
    }

    public void BlockShooting()
    {
        shootingBlocked = true;
    }

    public void UnblockShooting()
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

    public void SaveAmmo()
    {
        PlayerPrefsManager.SetUpgrade(rocketAmmo, UpgradeController.upgradeID.ROCKET_AMMO_ABS);
        PlayerPrefsManager.SetUpgrade(laserAmmo, UpgradeController.upgradeID.PHASER_AMMO_ABS);
    }
}
