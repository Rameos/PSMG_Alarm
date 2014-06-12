using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour
{
    public GameObject playerSubmarine;
    public GameObject[] rocket;
    public GameObject[] laser;
    private GameOverScript gameOver; 
    public float speed = 19f;
    public enum weaponTyps { rocket, laser };
    public weaponTyps weaponTyp = weaponTyps.rocket;
    
    

    private MovePlayer uboot;

    // Use this for initialization
    void Awake()
    {
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
        uboot = transform.root.GetComponent<MovePlayer>();
    }

    // Update is called once per frame

    void Update()
    {
        
          Vector3 aimPositon = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        //Vector3 aimPositon = Input.mousePosition;
          Debug.Log("AimpositionGaze " + aimPositon);
          Debug.Log("Aimpos Mouse " + Input.mousePosition);
        aimPositon.z = 0.0f;
        //Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
       aimPositon.x = aimPositon.x - ubootposition.x;
        //Für Mouse
       //aimPositon.y = aimPositon.y - ubootposition.x;
        //Für Eyetracking
        aimPositon.y = (Screen.height-aimPositon.y) - ubootposition.y;


        float angle = Mathf.Atan2(aimPositon.y, aimPositon.x) * Mathf.Rad2Deg - 90;

        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);

        if (Input.GetKeyDown("1"))
        {
            weaponTyp = weaponTyps.rocket;
            Debug.Log("Waffe gewechselt" + weaponTyp);
        }
        else if (Input.GetKeyDown("2"))
        {
            weaponTyp = weaponTyps.laser;
            Debug.Log("Waffe2Gewechselt" + weaponTyp);
        }

        if (Input.GetButtonDown("Fire1") && !gameOver.getGameOver())
        {
            switch (weaponTyp)
            {
                case (weaponTyps.rocket):
                    int ammoIndex = 0;
                    GameObject bulletInstance = (GameObject)Instantiate(rocket[ammoIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                    bulletInstance.rigidbody2D.AddForce(bulletInstance.transform.right * 1000);
                    Debug.Log("bulletForce: " + bulletInstance.transform.right + "bulletForce forward" + bulletInstance.transform.forward);
                    Destroy(bulletInstance, 2);
                    break;

                case (weaponTyps.laser):
                    Debug.Log("Laser");
                    int ammoIndexLaser=0;
                    GameObject laserInstance = (GameObject)Instantiate(laser[ammoIndexLaser], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    laserInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                    laserInstance.rigidbody2D.AddForce(laserInstance.transform.right * 1000);
                    Destroy(laserInstance, 2);
                    break;


            }

        }
    }
   
}
