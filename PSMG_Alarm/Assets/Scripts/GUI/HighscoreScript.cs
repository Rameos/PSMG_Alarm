using UnityEngine;
using System.Collections;

public class HighscoreScript : MonoBehaviour {

	public GUIText scoreText;

	private int score;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addScoreValue(int value) {
		score = score + value;
		changeScoreText ();
	}

	void changeScoreText() {
		scoreText.text = "" + score;
	}
}
