using UnityEngine;
using System.Collections;

public class SkilltreeGui : MonoBehaviour {

    public Texture2D coin;
    public GUIStyle coinsCountStyle;
    public GUIStyle titleStyle;
    public GUIStyle upgradeDescription;
    public GUIStyle descriptionText;

    private readonly int optimizedWidth = 1024;
    private readonly int optimizedHeight = 768;

    private string description;
    private string cost;
    private string name;

    private int standardTitleSize;
    private int standardCointextSize;

    private bool hovering;
    private GameObject canvas;

    void Start()
    {
        standardTitleSize = titleStyle.fontSize;
        standardCointextSize = coinsCountStyle.fontSize;
    }

    void OnGUI()
    {
        float ratioX = (float)Screen.width / (float)optimizedWidth;
        float ratioY = (float)Screen.height / (float)optimizedHeight;

        titleStyle.fontSize = Mathf.RoundToInt(standardTitleSize * ratioY);
        titleStyle.alignment = TextAnchor.UpperCenter;

        coinsCountStyle.fontSize = Mathf.RoundToInt(standardCointextSize * ratioY);
        coinsCountStyle.alignment = TextAnchor.UpperCenter;

        GUI.Label(new Rect(5, 10, 100, 100), coin);
        GUI.Box(new Rect(105, 40, 50, 30), "X 34", coinsCountStyle);

        GUI.Box(new Rect(Screen.width / 2 - 150, 10, 300, 50), "Get new gear!", titleStyle);

        if (hovering)
        {
            GUILayout.BeginArea(new Rect(Input.mousePosition.x - 30, Screen.height - Input.mousePosition.y - 170, 250, 250));
            {
                GUILayout.BeginVertical(upgradeDescription); // also can put width in here
                {
                    GUILayout.Label(name + " (" + cost +")", descriptionText);
                    GUILayout.Label(description, descriptionText);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }

    public void hoverUpgrade(string description, int cost, string name)
    {
        this.description = description;
        this.name = name;
        this.cost = cost.ToString();

        hovering = true;
    }

    public void stopHover()
    {
        hovering = false;
    }
}
