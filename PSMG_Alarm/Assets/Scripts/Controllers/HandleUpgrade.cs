using UnityEngine;
using System.Collections;

public class HandleUpgrade : MonoBehaviour
{

    public Sprite s_highlighted;
    public Sprite s_upgraded;

    public string title;
    public string description;
    public int cost;

    public bool not_upgradeable = false;
    public bool upgraded;
    public UpgradeController.upgradeID preCondition;
    public UpgradeController.upgradeID id;

    private Sprite s_nonHighlighted;
    private GameObject[] upgrades;
    private SkilltreeGui skillGUI;

    void Start()
    {
        s_nonHighlighted = GetComponent<SpriteRenderer>().sprite;
        skillGUI = GameObject.Find("Main Camera").GetComponent<SkilltreeGui>();

        int upgradedInt = PlayerPrefsManager.GetUpgrade(id);
        upgrades = GameObject.FindGameObjectsWithTag("Upgrade");

        if (upgradedInt == 1 && !not_upgradeable)
        {
            upgraded = true;
            GetComponent<SpriteRenderer>().sprite = s_upgraded;
        }
    }

    void OnMouseEnter()
    {
        bool available = false;

        if (preCondition != UpgradeController.upgradeID.NONE)
        {
            foreach (GameObject upgrade in upgrades)
            {
                HandleUpgrade handle = upgrade.GetComponent<HandleUpgrade>();
                if (handle.id == preCondition)
                {
                    if (handle.upgraded == true)
                    {
                        available = true;
                        break;
                    }
                }
            }
        }
        else
        {
            available = true;
        }

        if (!upgraded && available && PlayerPrefsManager.GetCoins() >= cost)
        {
            GetComponent<SpriteRenderer>().sprite = s_highlighted;
        }
            skillGUI.HoverUpgrade(description, cost, title);
    }

    void OnMouseExit()
    {
        if (!upgraded)
        {
            GetComponent<SpriteRenderer>().sprite = s_nonHighlighted;
        }

        skillGUI.StopHover();
    }

    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == s_highlighted && PlayerPrefsManager.GetCoins() > cost)
        {
            PlayerPrefsManager.SetCoins(PlayerPrefsManager.GetCoins() - cost);
            skillGUI.SendMessage("UpdateCoins");

            if (!not_upgradeable)
            {
                upgraded = true;
                GetComponent<SpriteRenderer>().sprite = s_upgraded;
            }

            if (id != UpgradeController.upgradeID.REPAIR)
            {
                PlayerPrefsManager.SetUpgrade(1, id);
            }
            else
            {
                if(PlayerPrefsManager.GetCurrentLife() < PlayerPrefsManager.GetMaxLife())
                {
                    PlayerPrefsManager.SetCurrentLive(PlayerPrefsManager.GetCurrentLife() + 1);
                    skillGUI.SendMessage("UpdateLife");
                }
            }

            Camera.main.GetComponent<SkilltreeGui>().UpdateAmmo();
        }
    }
}
