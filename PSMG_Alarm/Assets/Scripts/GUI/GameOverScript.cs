using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
    public GameObject destoyedPieces;

    private bool gameOver;
    private GameObject player;
    private GameControlScript controller;
    private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

    void Start()
    {
        gameOver = false;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("GameController").GetComponent<GameControlScript>();
    }

    public void EndOfGame()
    {
        gameOver = true;

        Screen.showCursor = true;
        PlayerPrefsManager.Reset();

        Instantiate(destoyedPieces, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        Destroy(player);
        controller.StopEnemies();
        controller.StopPowerUps();
    }

    void OnGUI()
    {
        if (gameOver) ShowGameOverMenu();
    }

    void ShowGameOverMenu()
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

    public bool GetGameOver()
    {
        return gameOver;
    }
}
