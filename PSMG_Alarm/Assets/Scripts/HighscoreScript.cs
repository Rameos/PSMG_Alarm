using UnityEngine;
using System.Collections;

public class HighscoreScript : MonoBehaviour {

	public GUIText scoreText;

	private int score;

	// Use this for initialization
	void Start () {
		score = 1234;
		changeScoreText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void addScoreValue(int value) {
		score = score + value;
	}

	void changeScoreText() {
		scoreText.text = "" + score;
	}
}
