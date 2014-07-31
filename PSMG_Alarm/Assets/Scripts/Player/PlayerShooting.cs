using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class PlayerShooting : MonoBehaviour
{
    public GameObject[] rocket;
    public GameObject[] laser;
	public GameObject damdam;
    private GameOverScript gameOver; 
    public float speed = 19f;
    public enum weaponTyps { rocket, laser };
    public weaponTyps weaponTyp = weaponTyps.rocket;
	private bool shootingBlocked;
    

    // Use this for initialization
    void Awake()
    {
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
    }

    // Update is called once per frame

    void Update()
    {
        
        //Vector3 aimPositon = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        Vector3 aimPositon = Input.mousePosition;

        aimPositon.z = 0.0f;
        Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
       	aimPositon.x = aimPositon.x - ubootposition.x;
        //Für Mouse
      	aimPositon.y = aimPositon.y - ubootposition.y;
        //Für Eyetracking
        //aimPositon.y = (Screen.height-aimPositon.y) - ubootposition.y;


        float angle = Mathf.Atan2(aimPositon.y, aimPositon.x) * Mathf.Rad2Deg - 90;

        Vector3 rotationVector = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotationVector);

        if (Input.GetKeyDown("1"))
        {
            weaponTyp = weaponTyps.rocket;
        }
        else if (Input.GetKeyDown("2"))
        {
            weaponTyp = weaponTyps.laser;
        }

		if (Input.GetButtonDown("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive && networkView.isMine && !shootingBlocked||Input.GetButtonDown("Fire1") && !gameOver.getGameOver() && NetworkManagerScript.networkActive==false && !shootingBlocked)
        {
            switch (weaponTyp)
            {
                case (weaponTyps.rocket):
				if(NetworkManagerScript.networkActive && networkView.isMine){
					GameObject bulletInstance = (GameObject)Network.Instantiate(damdam, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), 2);
					bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                    bulletInstance.rigidbody2D.AddForce(bulletInstance.transform.right * 1000);
                    Destroy(bulletInstance, 2);                    
				}
				if(NetworkManagerScript.networkActive==false){
					int ammoIndex = 0;
					GameObject bulletInstance = (GameObject)Instantiate(rocket[ammoIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
					bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
					bulletInstance.rigidbody2D.AddForce(bulletInstance.transform.right * 1000);
					Destroy(bulletInstance, 2);                    
				}
				
				break;
				
			case (weaponTyps.laser):
				if(NetworkManagerScript.networkActive && networkView.isMine){
                    int ammoIndexLaser=0;
					GameObject laserInstance = (GameObject)Network.Instantiate(laser[ammoIndexLaser], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)),3);
					laserInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                    laserInstance.rigidbody2D.AddForce(laserInstance.transform.right * 1000);
                    Destroy(laserInstance, 2);                    
				}
				if(NetworkManagerScript.networkActive==false){
					int ammoIndexLaser=0;
					GameObject laserInstance = (GameObject)Instantiate(laser[ammoIndexLaser], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
					laserInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
					laserInstance.rigidbody2D.AddForce(laserInstance.transform.right * 1000);
					Destroy(laserInstance, 2);                    
				}break;
					
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
}
					