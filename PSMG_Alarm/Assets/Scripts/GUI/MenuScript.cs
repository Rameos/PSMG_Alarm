using UnityEngine;
using System.Collections;
using iViewX;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public NetworkManagerScript networkScript;

	public GameObject mainMenuPanel; 
	public GameObject modiMenuPanel;
	public GameObject levelsOfDifficultyPanel;
	public GameObject multiplayerPanel; 
	public GameObject connectToHostButton;
    public GameObject settingsPanel;

	private bool mainMenu, modiMenu, levelsOfDifficulty, highscores, multiplayer;
	
	void Start()
	{
		PlayerPrefsManager.Reset();
		Screen.showCursor = true;
		mainMenuPanel.SetActive (true);
		modiMenuPanel.SetActive (false);
		levelsOfDifficultyPanel.SetActive (false);
		multiplayerPanel.SetActive (false);
        settingsPanel.SetActive(false);
	}

	public void onCalibrationClick() {
        GazeControlComponent.Instance().StartCalibration();
	}

	public void onQuitGameClick() {
		Application.Quit();
	}

	public void onEasyButtonClick() {
        PlayerPrefsManager.SetDifficulty(0);
		Application.LoadLevel("story_sequence");
	}

	public void onMiddleButtonClick() {
        PlayerPrefsManager.SetDifficulty(1);
		Application.LoadLevel("story_sequence");
	}

	public void onHardButtonClick() {
        PlayerPrefsManager.SetDifficulty(2);
		Application.LoadLevel("story_sequence");
	}

	public void onMultiplayerButtonClick() {
		
	}

	public void onHostGameButtonClick() {
		//networkScript.Server_startServer();
	}

	public void onStartMultiplayerGameButtonClick() {
		//networkScript.Client_refreshHostList();
	}
}