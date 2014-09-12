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
	public GameObject ConnectToHostButton;

	private bool mainMenu, modiMenu, levelsOfDifficulty, highscores, multiplayer;


	void Start()
	{
		PlayerPrefsManager.Reset();
		Screen.showCursor = true;
		mainMenuPanel.SetActive (true);
		modiMenuPanel.SetActive (false);
		levelsOfDifficultyPanel.SetActive (false);
		multiplayerPanel.SetActive (false);

	}

	public void onStartGameClick() {
		Debug.Log ("startGame");
	}

	public void onHighscoresClick() {
		Debug.Log ("Highscore");
	}

	public void onCalibrationClick() {
		GazeControlComponent.Instance.StartCalibration();
	}

	public void onQuitGameClick() {
		Application.Quit();
	}

	public void onEasyButtonClick() {
		Application.LoadLevel("story_sequence");
	}

	public void onMiddleButtonClick() {
		Application.LoadLevel("story_sequence");
	}

	public void onHardButtonClick() {
		Application.LoadLevel("story_sequence");
	}

	public void onMultiplayerButtonClick() {
		if (networkScript.Client_getHostDataStatus())
		{
			HostData[] list = networkScript.Client_getHostData();
			for (int i = 0; i < list.Length; i++)
			{
				Button newItem = (Button)Instantiate(ConnectToHostButton);
				newItem.name = list[i].gameName;
				newItem.onClick.AddListener(() => {networkScript.Client_connectToHost(list[i]); });
			}
		}
	}

	public void onHostGameButtonClick() {
		networkScript.Server_startServer();
	}

	public void onStartMultiplayerGameButtonClick() {
		networkScript.Client_refreshHostList();
	}
	
}
