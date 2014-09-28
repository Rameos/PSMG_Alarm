using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServlistitemScript : MonoBehaviour {

	public Text gameName;
	public Text gameDescription;
	public Text gameDifficulty;



	// Use this for initialization
	void Start () {
	}

	public void SetGameName(string input){
		gameName.text = input;
	}
	public void SetGameDescription(string input){
		gameDescription.text = input;
	}
	public void SetGameDifficulty(string input){
		gameDifficulty.text = input;
	}
}
