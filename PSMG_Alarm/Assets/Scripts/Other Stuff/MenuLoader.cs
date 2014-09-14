using UnityEngine;
using System.Collections;

public class MenuLoader : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            Application.LoadLevel("main_menu");
        }
    }
}
