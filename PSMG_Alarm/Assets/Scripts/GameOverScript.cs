using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GUIText gameOverText;
	public GUITexture tryAgainBut;
	public GUITexture mainMenuBut;

	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endOfGame() {
		gameOverText.enabled = true;
	}
}
