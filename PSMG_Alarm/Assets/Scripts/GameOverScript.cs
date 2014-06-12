using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public MovePlayer movePlayer;

	public GUIText gameOverText;
	public GUITexture tryAgainBut;
	public GUITexture mainMenuBut;
	public GUIText tryAgainText;
	public GUIText mainMenuText;


	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;
		tryAgainBut.enabled = false;
		mainMenuBut.enabled = false;
		mainMenuText.enabled = false;
		tryAgainText.enabled = false;
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void endOfGame() {
		gameOverText.enabled = true;
		tryAgainBut.enabled = true;
		mainMenuBut.enabled = true;
		mainMenuText.enabled = true;
		tryAgainText.enabled = true;

		movePlayer.disablePlayerMovement();
	}
}
