using UnityEngine;
using System.Collections;

public class MenuLoader : MonoBehaviour
{
    void Start()
    {
        Screen.showCursor = true;
    }

    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        Application.LoadLevel("main_menu");
    }
}
