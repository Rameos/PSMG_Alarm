﻿using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	//private GUIText gameOverText;
	//private GUITexture tryAgainBut;
	//private GUITexture mainMenuBut;
    private GameObject[] enemies;
    private GameObject[] powerUps; 
    private MovePlayer movePlayer;
    private bool gameOver;

    private bool mainMenu, modiMenu, levelsOfDifficulty, highscores;
    private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

	// Use this for initialization
	void Start () {
		//gameOverText.enabled = false;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endOfGame() {

        gameOver = true;
        EnemyMovement enemyMovement;
        PowerUpMovement powerUpMovement;
       // gameOverText.enabled = true;

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
