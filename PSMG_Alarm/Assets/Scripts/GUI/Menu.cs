using UnityEngine;
using System.Collections;
using iViewX;

public class Menu : MonoBehaviour
{
    public NetworkManagerScript networkScript;

    private bool mainMenu, modiMenu, levelsOfDifficulty, highscores, multiplayer, host, lobby, join, startGameButton;
    private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

	private int serverSelection;
	private HostData[] list;

	private string hostFeedBackText, serverName, serverDescription;

    void Start()
    {
        PlayerPrefsManager.Reset();
        Screen.showCursor = true;

        mainMenu = true;
        modiMenu = false;
        levelsOfDifficulty = false;
        highscores = false;
		host = false;
		startGameButton = false;
		lobby = false;
		join = false;

		serverSelection = -1;

		hostFeedBackText = "Setting up the server..";
		serverName = "Type in server name";
		serverDescription = "Type in server Description";
    }

    void OnGUI()
    {
        buttonWidth = Screen.width / 6;
        buttonHeight = Screen.height / 12;
        centerX = Screen.width / 2;
        centerY = Screen.height / 2;
        guiBoxWidth = Screen.width / 3;
        guiBoxHeight = Screen.height / 2;
        guiBoxX = centerX - guiBoxWidth / 2;
        guiBoxY = centerY - guiBoxHeight / 2;

        if (mainMenu) InitMainMenu();
        if (modiMenu) InitModiMenu();
        if (levelsOfDifficulty) InitLevelsOfDifficultyMenu();
        if (highscores) InitHighscores();
        if (multiplayer) InitMultiplayer();
		if (host) InitHostServer ();
		if (lobby) InitLobby ();
		if (join) InitJoinServer ();


    }

    void InitMainMenu()
    {

        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Hauptmenü");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "Spiel Starten"))
        {
            mainMenu = false;
            modiMenu = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "Highscores"))
        {
            mainMenu = false;
            highscores = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight, buttonWidth, buttonHeight), "Kalibrierung"))
        {
            GazeControlComponent.Instance.StartCalibration();
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 4 * buttonHeight, buttonWidth, buttonHeight), "Spiel beenden"))
        {
            Application.Quit();
        }
    }

    void InitModiMenu()
    {
        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Modi");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "Einzelspieler"))
        {
            modiMenu = false;
            levelsOfDifficulty = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "Koop"))
        {
            modiMenu = false;
            multiplayer = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight, buttonWidth, buttonHeight), "Versus"))
        {
            Application.LoadLevel("submarine");
        }
        if (GUI.Button(new Rect(guiBoxX, guiBoxY, buttonWidth / 3, buttonHeight), "<"))
        {
            modiMenu = false;
            mainMenu = true;
        }
    }

    void InitLevelsOfDifficultyMenu()
    {
        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Schwierigkeitsstufen");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "leicht"))
        {
            Application.LoadLevel("story_sequence");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "mittel"))
        {
            Application.LoadLevel("story_sequence");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight, buttonWidth, buttonHeight), "schwer"))
        {
            Application.LoadLevel("story_sequence");
        }
        if (GUI.Button(new Rect(guiBoxX, guiBoxY, buttonWidth / 3, buttonHeight), "<"))
        {
            levelsOfDifficulty = false;
            modiMenu = true;
        }
    }

    void InitHighscores()
    {
        GUI.Label(new Rect(centerX, guiBoxY, buttonWidth, buttonHeight * 2), "Highscores");
        if (GUI.Button(new Rect(guiBoxX, guiBoxY, buttonWidth / 3, buttonHeight), "<"))
        {
            highscores = false;
            mainMenu = true;
        }
    }

    void InitMultiplayer()
    {
        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Server");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "Spiel Hosten"))
        {            
			host = true;
			multiplayer = false;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "Spiele anzeigen"))
        {
			join = true;
			multiplayer = false;
            networkScript.Client_refreshHostList();
        }
        if (GUI.Button(new Rect(guiBoxX, guiBoxY, buttonWidth / 3, buttonHeight), "<"))
        {
            multiplayer = false;
            modiMenu = true;
        }
    }

	void InitHostServer() {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Hosting Server");

		serverName = GUI.TextField (new Rect (guiBoxX, guiBoxY+30, guiBoxWidth, 20), serverName);
		serverDescription = GUI.TextField (new Rect (guiBoxX, guiBoxY+60, guiBoxWidth, 20), serverDescription);
		if (GUI.Button(new Rect(guiBoxX, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Abbrechen"))
		{
			host = false;
			multiplayer = true;
		}
		if (GUI.Button(new Rect(guiBoxX+buttonWidth, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Spiel hosten"))
		{
			networkScript.Server_startServer(serverName, serverDescription);
			lobby = true;
			host = false;
		}
	}

	void InitLobby() {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Hosting Server");
		if (GUI.Button(new Rect(guiBoxX, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Abbrechen"))
		{
			networkScript.Server_UnregisterServer();
			lobby = false;
			multiplayer = true;
			ChangeHostFeedBackText("Setting up the server..");
		}
		GUI.enabled = startGameButton;
		GUI.TextArea(new Rect (guiBoxX, guiBoxY+30, guiBoxWidth, guiBoxHeight-30-buttonHeight), hostFeedBackText);
		if (GUI.Button(new Rect(guiBoxX+buttonWidth, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Spiel starten"))
		{
			networkScript.Server_LoadLevel();
		}
		GUI.enabled = true;	
	}

	void InitJoinServer() {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Joining Server");



		if (networkScript.Client_getHostDataStatus())
		{
			list = networkScript.Client_getHostData();

			if(list != null){
				string[] serverNames = new string[list.Length];
				for (int i = 0; i < list.Length; i++)
				{
					serverNames[i] = list[i].gameName;
				}
				serverSelection = GUI.SelectionGrid(new Rect (guiBoxX, guiBoxY+buttonHeight, guiBoxWidth, guiBoxHeight-buttonHeight-buttonHeight), serverSelection, serverNames, 1);
			}else {
				GUI.Label (new Rect (guiBoxX, guiBoxY+buttonHeight, guiBoxWidth, 20), "no servers are running right now");
			}
		}else{
			GUI.Label (new Rect (guiBoxX, guiBoxY+buttonHeight, guiBoxWidth, 20), "refreshing...");
		}

		if (GUI.Button(new Rect(guiBoxX, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Aktualisieren"))
		{
			networkScript.Client_refreshHostList();
		}
		GUI.enabled = false;
		if (serverSelection != -1) {GUI.enabled = true;}
		if (GUI.Button(new Rect(guiBoxX+buttonWidth, guiBoxY+guiBoxHeight-buttonHeight, buttonWidth, buttonHeight), "Beitreten"))
		{
			networkScript.Client_connectToHost(list[serverSelection]); 
		}
		GUI.enabled = true;
		if (GUI.Button(new Rect(guiBoxX, guiBoxY, buttonWidth / 3, buttonHeight), "<"))
		{
			join = false;
			multiplayer = true;
		}
	}

	public void ChangeHostFeedBackText(string input) {
		hostFeedBackText = input;
	}

	public void EnableStartButton(){
		startGameButton = true;
	}
}

