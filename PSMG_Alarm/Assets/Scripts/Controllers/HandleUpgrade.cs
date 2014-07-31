using UnityEngine;
using System.Collections;

public class HandleUpgrade : MonoBehaviour {

    public Sprite s_highlighted;
    public Sprite s_upgraded;

    public string title;
    public string description;
    public int cost;
    public UpgradeController.upgradeID id;
    
    private Sprite s_nonHighlighted;

    private bool upgraded;
    private SkilltreeGui skillGUI;

	void Start () 
    {
        s_nonHighlighted = GetComponent<SpriteRenderer>().sprite;
        skillGUI = GameObject.Find("Main Camera").GetComponent<SkilltreeGui>();
	}

    void OnMouseEnter()
    {
        if (!upgraded)
        {
            GetComponent<SpriteRenderer>().sprite = s_highlighted;
            skillGUI.hoverUpgrade(description, cost, title);
        }
    }

    void OnMouseExit()
    {
        if (!upgraded)
        {
            GetComponent<SpriteRenderer>().sprite = s_nonHighlighted;
        }

        skillGUI.stopHover();
    }

    void OnMouseDown()
    {
        upgraded = true;
        GetComponent<SpriteRenderer>().sprite = s_upgraded;
    }
}
