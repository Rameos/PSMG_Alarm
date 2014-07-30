using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	private GameObject[] powerUps,particleEmitter;
    private MovePlayer movePlayer;
    private bool gameOver;

    private bool mainMenu, modiMenu, levelsOfDifficulty, highscores;
    private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

	void Start () {
        gameOver = false;
	}

	void Update () {
	}

	public void endOfGame() {
        gameOver = true;

        powerUps = GameObject.FindGameObjectsWithTag("PowerUp"); 
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
		particleEmitter = GameObject.FindGameObjectsWithTag("MovementParticles");
        
        movePlayer.stopPlayerMovement();
		stopEnemies (); 
		stopPowerUps ();
		stopParticles ();
	}

	void stopEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy controller = enemy.GetComponent<Enemy>();
            controller.stopEnemyMovement();
        }
	}

	void stopParticles() {
		Debug.Log ("stopping particles");
		for (int i = 0; i < particleEmitter.Length; i++) {
			particleEmitter[i].GetComponent<ParticleSystem>().Stop(); 
		}
	}

	void stopPowerUps() {
		PowerUpMovement powerUpMovement;
		for (int i = 0; i < powerUps.Length; i++)
		{
			powerUpMovement = powerUps[i].GetComponent<PowerUpMovement>(); 
			powerUpMovement.stopPowerUpMovement(); 
		}
	}

    void OnGUI()
    {
        if (gameOver) showGameOverMenu(); 
    }

    void showGameOverMenu()
    {
        buttonWidth = Screen.width / 6;
        buttonHeight = Screen.height / 12;
        centerX = Screen.width / 2;
        centerY = Screen.height / 2;
        guiBoxWidth = Screen.width / 3;
        guiBoxHeight = Screen.height / 2;
        guiBoxX = centerX - guiBoxWidth / 2;
        guiBoxY = centerY - guiBoxHeight / 2;

        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Game Over");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "Nochmal"))
        {
            Application.LoadLevel("submarine");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "Haupmenü"))
        {
            Application.LoadLevel("main_menu");
        }
    }

    public bool getGameOver()
    {
        return gameOver; 
    }
}
