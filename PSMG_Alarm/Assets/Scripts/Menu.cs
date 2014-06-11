using UnityEngine;
using System.Collections;
using iViewX;

public class Menu : MonoBehaviour
{

    bool mainMenu, modiMenu, levelsOfDifficulty, highscores;
    int buttonWidth, buttonHeight, centerX, centerY, guiBoxWidth, guiBoxHeight, guiBoxX, guiBoxY;

    //Use this for initialization
    void Start()
    {
        mainMenu = true;
        modiMenu = false;
        levelsOfDifficulty = false;
        highscores = false;
    }

    // Update is called once per frame
    void Update()
    {

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

        if (mainMenu) initMainMenu();
        if (modiMenu) initModiMenu();
        if (levelsOfDifficulty) initLevelsOfDifficultyMenu();
        if (highscores) initHighscores();

    }

    void initMainMenu()
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
        }
    }

    void initModiMenu()
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
            levelsOfDifficulty = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight, buttonWidth, buttonHeight), "Versus"))
        {
            Application.LoadLevel("submarine");
        }

    }

    void initLevelsOfDifficultyMenu()
    {
        GUI.Box(new Rect(guiBoxX, guiBoxY, guiBoxWidth, guiBoxHeight), "Schwierigkeitsstufen");

        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + buttonHeight, buttonWidth, buttonHeight), "leicht"))
        {
            Application.LoadLevel("submarine");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 2 * buttonHeight, buttonWidth, buttonHeight), "mittel"))
        {
            Application.LoadLevel("submarine");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2, guiBoxY + 3 * buttonHeight, buttonWidth, buttonHeight), "schwer"))
        {
            Application.LoadLevel("submarine");
        }
    }

    void initHighscores()
    {

    }
}

