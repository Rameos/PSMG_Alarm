using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private Vector3 targetLocation;

    public GameObject camera2d;
	public GameObject explosion; 

    private EnemySpawner spawner;
	private HighscoreScript highscorecontroller;
	private SubmarineLifeControl submarineLifeControl;
    private GameOverScript gameOver; 
    private bool moveAllowed = true;
    private float speed = 2;


	void Start () 
    {
        camera2d = GameObject.Find("2D Camera");
        spawner = GameObject.Find("GameController").GetComponent<EnemySpawner>();
        GetNewTargetLocation();
		highscorecontroller = GameObject.FindObjectOfType(typeof(HighscoreScript)) as HighscoreScript;
		submarineLifeControl = GameObject.FindObjectOfType(typeof(SubmarineLifeControl)) as SubmarineLifeControl;
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();

	}
	

	void Update () 
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
		TurnToTarget();
		
		transform.position = transform.position + transform.right * speed * Time.deltaTime;
		if (Vector3.Distance(transform.position, targetLocation) < 1)
			GetNewTargetLocation();
	}
	
	void TurnToTarget()
	{
		float angle = Vector3.Angle(transform.position - targetLocation, -transform.right);
		if (angle > 20) 
			transform.Rotate(Vector3.forward, 10);
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Rocket") {
			highscorecontroller.addScoreValue(100);
			Destroy (gameObject);
			Destroy (col.gameObject);
			if(!gameOver.getGameOver()) spawner.SpawnEnemy ();
		} else if (col.gameObject.tag == "Player") {
			highscorecontroller.addScoreValue(100);
			submarineLifeControl.decrementLife ();
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (gameObject);
			if(!gameOver.getGameOver())spawner.SpawnEnemy();
		} else if (col.gameObject.tag == "Shield") {
			Destroy (GameObject.Find("Shield(Clone)"));
			highscorecontroller.addScoreValue(100);
			Destroy (gameObject);
			if(!gameOver.getGameOver())spawner.SpawnEnemy();
		} else if (col.gameObject.tag == "Wave") {
			highscorecontroller.addScoreValue(100);
			Destroy (gameObject);
			if(!gameOver.getGameOver())spawner.SpawnEnemy();
		}
    }

    public void stopEnemyMovement()
    {
        moveAllowed = false;
		Debug.Log ("stop"); 
    }

    public bool getMoveAllowed()
    {
        return moveAllowed; 
    }

	public void setSpeed(float value) {
		Debug.Log (value); 
		speed = value; 
	}
}
