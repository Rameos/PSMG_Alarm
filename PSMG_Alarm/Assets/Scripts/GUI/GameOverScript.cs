using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
    private MovePlayer movePlayer;
    private bool gameOver;
    private GameControlScript controller;
    private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

    void Start()
    {
        gameOver = false;
        controller = GameObject.Find("GameController").GetComponent<GameControlScript>();
    }

    public void endOfGame()
    {
        gameOver = true;
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();

        movePlayer.stopPlayerMovement();
        controller.stopEnemies();
        controller.stopPowerUps();
        controller.stopParticles();
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
