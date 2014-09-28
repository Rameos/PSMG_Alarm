using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
    public GameObject destoyedPieces;
	public GameObject gameOverPanel;

    private bool gameOver;
    private GameObject player;
    private GameControlScript controller;

   // private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

    void Start()
    {
        gameOver = false;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("GameController").GetComponent<GameControlScript>();
		gameOverPanel.SetActive (false);
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
		gameOverPanel.SetActive (true);
    }

	public void onPlayAgainButtonClick() {
		Application.LoadLevel("submarine");
	}
	
	public void onToMainmenuButtonClick() {
		Application.LoadLevel("main_menu");
	}

    public bool GetGameOver()
    {
        return gameOver;
    }
}