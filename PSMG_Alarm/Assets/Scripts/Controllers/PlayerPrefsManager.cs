using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{

    // store selected difficulty
    private static readonly string DIFFICULTY = "difficulty";

    // store current level
    private static readonly string CURRENT_LEVEL = "current_level";

    // store current score
    private static readonly string CURRENT_SCORE = "current_score";

    // store current coins
    private static readonly string CURRENT_COINS = "current_coins";

    // store player life stats
    private static readonly string PLAYER_CURRENT_LIFES = "player_current_lifes";
    private static readonly string PLAYER_MAX_LIFES = "player_max_lifes";

    // store bought upgrades
    private static readonly string MG_UPGRADE_1 = "mg_upgrade_1";
    private static readonly string MAX_LIVE_UPGRADE = "max_live_upgrade";

    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SetDifficulty(int value)
    {
        PlayerPrefs.SetInt(DIFFICULTY, value);
    }

    public static void SetLevel(int value)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL, value);
    }

    public static void SetScore(int value)
    {
        PlayerPrefs.SetInt(CURRENT_SCORE, value);
    }

    public static void SetCoins(int value)
    {
        PlayerPrefs.SetInt(CURRENT_COINS, value);
    }

    public static void SetCurrentLive(int value)
    {
        PlayerPrefs.SetInt(PLAYER_CURRENT_LIFES, value);
    }

    public static void SetMaxLives(int value)
    {
        PlayerPrefs.SetInt(PLAYER_MAX_LIFES, value);
    }
    
    public static void SetUpgrade(int value, UpgradeController.upgradeID id)
    {
        switch (id)
        {
            case UpgradeController.upgradeID.MAX_LIVE:
                PlayerPrefs.SetInt(MAX_LIVE_UPGRADE, value);
                break;
            case UpgradeController.upgradeID.MACHINE_GUN_1:
                PlayerPrefs.SetInt(MG_UPGRADE_1, value);
                break;
        }
    }
}
