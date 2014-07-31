using UnityEngine;
using System.Collections;

public class SkilltreeGui : MonoBehaviour
{
    public GUIStyle coinsCountStyle;
    public GUIStyle titleStyle;
    public GUIStyle upgradeDescription;
    public GUIStyle descriptionText;

    public static readonly int optimizedHeight = 768;
    public static readonly int optimizedWidth = 1024;

    private GameObject ammoPhaser;
    private GameObject ammoRocket;
    private GameObject coin;

    private string description;
    private string cost;
    private string title;

    private int standardTitleSize;
    private int standardCointextSize;

    private float standardCanvasSizeY;
    private float standardCanvasSizeX;

    private bool hovering;

    void Start()
    {
        standardTitleSize = titleStyle.fontSize;
        standardCointextSize = coinsCountStyle.fontSize;

        ammoPhaser = GameObject.Find("AmmoItemPhaser");
        ammoRocket = GameObject.Find("AmmoItemRocket");
        coin = GameObject.Find("Coin");
    }


    void OnGUI()
    {
        float ratioY = (float)Screen.height / (float)optimizedHeight;

        titleStyle.fontSize = Mathf.RoundToInt(standardTitleSize * ratioY);
        titleStyle.alignment = TextAnchor.UpperCenter;

        coinsCountStyle.fontSize = Mathf.RoundToInt(standardCointextSize * ratioY);

        GUI.Box(new Rect(camera.WorldToScreenPoint(coin.transform.position).x + 30 * Screen.height / 500,
            Screen.height - camera.WorldToScreenPoint(coin.transform.position).y - 15 + Screen.height / 500, 50, 30),
            "x 134", coinsCountStyle);

        coinsCountStyle.alignment = TextAnchor.UpperLeft;

        GUI.Box(new Rect(camera.WorldToScreenPoint(ammoPhaser.transform.position).x - 15 * Screen.height / 500,
            camera.WorldToScreenPoint(ammoPhaser.transform.position).y + 50 * Screen.height / 500, 50, 30),
            "34", coinsCountStyle);

        GUI.Box(new Rect(camera.WorldToScreenPoint(ammoRocket.transform.position).x - 15 * Screen.height / 500,
            camera.WorldToScreenPoint(ammoRocket.transform.position).y + 50 * Screen.height / 500, 50, 30),
            "34", coinsCountStyle);

        GUI.Box(new Rect(Screen.width / 13, Screen.height / 20, 300, 100), "Prepare For Your Next Mission...", titleStyle);

        if (hovering)
        {
            GUILayout.BeginArea(new Rect(Input.mousePosition.x - 30, Screen.height - Input.mousePosition.y - 170, 250, 250));
            {
                GUILayout.BeginVertical(upgradeDescription); // also can put width in here
                {
                    GUILayout.Label(title + " (" + cost + ")", descriptionText);
                    GUILayout.Label(description, descriptionText);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }

    public void hoverUpgrade(string description, int cost, string title)
    {
        this.description = description;
        this.title = title;
        this.cost = cost.ToString();

        hovering = true;
    }

    public void stopHover()
    {
        hovering = false;
    }
}
