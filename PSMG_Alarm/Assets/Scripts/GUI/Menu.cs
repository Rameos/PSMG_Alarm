using UnityEngine;
using System.Collections;
using iViewX;

public class Menu : MonoBehaviour
{
    public NetworkManagerScript networkScript;

    private bool mainMenu, modiMenu, levelsOfDifficulty, highscores, multiplayer;
	private int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY, backbuttonWidth, backbuttonHeight;
	private GUIStyle boxStyle, buttonStyle;

    void Start()
    {
        PlayerPrefsManager.Reset();
        Screen.showCursor = true;

        mainMenu = true;
        modiMenu = false;
        levelsOfDifficulty = false;
        highscores = false;
    }

    void OnGUI()
	{ 	GUI.backgroundColor = new Color(2,2,4,255);
	 	boxStyle = new GUIStyle (GUI.skin.box);
		boxStyle.fontSize = 25;
		boxStyle.padding = new RectOffset(10,10,10,0);
		boxStyle.margin = new RectOffset(20,20,20,0);
		buttonStyle = new GUIStyle (GUI.skin.button);
		buttonStyle.fontSize = 20;


		buttonWidth = Screen.width / 3;
        buttonHeight = Screen.height / 12;
		backbuttonWidth = buttonWidth / 5;
		backbuttonHeight = buttonWidth / 5;
        centerX = Screen.width / 2;
        centerY = Screen.height / 2;
        guiBoxWidth = Screen.width / 2;
		guiBoxHeight = Screen.height / 2 + buttonHeight;
		guiBoxX = centerX - guiBoxWidth / 2;
        guiBoxY = centerY - guiBoxHeight / 2;

        if (mainMenu) InitMainMenu();
        if (modiMenu) InitModiMenu();
        if (levelsOfDifficulty) InitLevelsOfDifficultyMenu();
        if (highscores) InitHighscores();
        if (multiplayer) InitMultiplayer();

    }

    void InitMainMenu()
    {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Hauptmenü", boxStyle);

		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight + 20, buttonWidth, buttonHeight), "Spiel Starten", buttonStyle))
        {
            mainMenu = false;
            modiMenu = true;
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight + 30, buttonWidth, buttonHeight), "Highscores", buttonStyle))
        {
            mainMenu = false;
            highscores = true;
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight + 40, buttonWidth, buttonHeight), "Kalibrierung", buttonStyle))
        {
            GazeControlComponent.instanceObject.StartCalibration();
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 4 * buttonHeight + 50, buttonWidth, buttonHeight), "Spiel beenden", buttonStyle))
        {
            Application.Quit();
        }
    }

    void InitModiMenu()
    {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Modi", boxStyle);

		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight + 20, buttonWidth, buttonHeight), "Einzelspieler", buttonStyle))
        {
            modiMenu = false;
            levelsOfDifficulty = true;
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight + 30, buttonWidth, buttonHeight), "Koop", buttonStyle))
        {
            modiMenu = false;
            multiplayer = true;
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight + 40, buttonWidth, buttonHeight), "Versus", buttonStyle))
        {
            Application.LoadLevel("submarine");
        }
		if (GUI.Button(new Rect(guiBoxX + 5, guiBoxY + 5, backbuttonWidth, backbuttonHeight), "<", buttonStyle))
        {
            modiMenu = false;
            mainMenu = true;
        }
    }

    void InitLevelsOfDifficultyMenu()
    {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Schwierigkeitsstufen", boxStyle);

		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight+ 20, buttonWidth, buttonHeight), "leicht", buttonStyle))
        {
            Application.LoadLevel("story_sequence");
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight + 30, buttonWidth, buttonHeight), "mittel", buttonStyle))
        {
            Application.LoadLevel("story_sequence");
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight + 40, buttonWidth, buttonHeight), "schwer", buttonStyle))
        {
            Application.LoadLevel("story_sequence");
        }
		if (GUI.Button(new Rect(guiBoxX + 5, guiBoxY+ 5, backbuttonWidth, backbuttonHeight), "<",buttonStyle))
        {
            levelsOfDifficulty = false;
            modiMenu = true;
        }
    }

    void InitHighscores()
    {
        GUI.Label(new Rect(centerX, guiBoxY, buttonWidth, buttonHeight * 2), "Highscores");
		if (GUI.Button(new Rect(guiBoxX, guiBoxY, backbuttonWidth, backbuttonHeight), "<", buttonStyle))
        {
            highscores = false;
            mainMenu = true;
        }
    }

    void InitMultiplayer()
    {
		GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Server", boxStyle);

		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight + 20, buttonWidth, buttonHeight), "Spiel Hosten", buttonStyle))
        {
            networkScript.Server_startServer();
        }
		if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight + 30, buttonWidth, buttonHeight), "Spiele anzeigen", buttonStyle))
        {
            networkScript.Client_refreshHostList();
            //networkScript.Client_connectToHost();
        }
        if (networkScript.Client_getHostDataStatus())
        {
            HostData[] list = networkScript.Client_getHostData();
            for (int i = 0; i < list.Length; i++)
            {
                if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + (2 + i + 1) * buttonHeight, buttonWidth, buttonHeight), list[i].gameName))
                {
                    networkScript.Client_connectToHost(list[i]);
                }
            }
        }
		if (GUI.Button(new Rect(guiBoxX + 5, guiBoxY + 5, backbuttonWidth, backbuttonHeight), "<", buttonStyle))
        {
            multiplayer = false;
            modiMenu = true;
        }
    }
}