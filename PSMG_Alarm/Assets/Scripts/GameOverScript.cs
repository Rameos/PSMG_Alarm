using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GUIText gameOverText;
	public GUITexture tryAgainBut;
	public GUITexture mainMenuBut;
    private GameObject[] enemies;
    private GameObject[] powerUps; 
    private MovePlayer movePlayer;
    private bool gameOver; 

	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;
        gameOver = false; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endOfGame() {
        gameOver = true; 
        EnemyMovement enemyMovement; 
        PowerUpMovement powerUpMovement; 
		gameOverText.enabled = true;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp"); 
        movePlayer = GameObject.Find("Submarine").GetComponent<MovePlayer>();
        
        movePlayer.stopPlayerMovement();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyMovement = enemies[i].GetComponent<EnemyMovement>();
            enemyMovement.stopEnemyMovement(); 
        }
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>(); 
            powerUpMovement.stopPowerUpMovement(); 
        }
	}

    public bool getGameOver()
    {
        return gameOver; 
    }
}
