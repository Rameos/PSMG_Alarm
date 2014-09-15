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
    private static readonly string MG_UPGRADE_2 = "mg_upgrade_2";
    private static readonly string PHASER_UPGRADE = "phaser_upgrade";
    private static readonly string ROCKET_UPGRADE = "rocket_upgrade";
    private static readonly string PHASER_AMMO = "phaser_ammo";
    private static readonly string ROCKET_AMMO = "rocket_ammo";
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
            case UpgradeController.upgradeID.MACHINE_GUN_2:
                PlayerPrefs.SetInt(MG_UPGRADE_2, value);
                break;
            case UpgradeController.upgradeID.PHASER:
                PlayerPrefs.SetInt(PHASER_UPGRADE, value);
                break;
            case UpgradeController.upgradeID.ROCKET:
                PlayerPrefs.SetInt(ROCKET_UPGRADE, value);
                break;
            case UpgradeController.upgradeID.ROCKET_AMMO:
                PlayerPrefs.SetInt(ROCKET_AMMO, GetUpgrade(UpgradeController.upgradeID.ROCKET_AMMO) + 1);
                break;
            case UpgradeController.upgradeID.PHASER_AMMO:
                PlayerPrefs.SetInt(PHASER_AMMO, GetUpgrade(UpgradeController.upgradeID.PHASER_AMMO) + 1);
                break;
            case UpgradeController.upgradeID.ROCKET_AMMO_ABS:
                PlayerPrefs.SetInt(ROCKET_AMMO, value);
                break;
            case UpgradeController.upgradeID.PHASER_AMMO_ABS:
                PlayerPrefs.SetInt(PHASER_AMMO, value);
                break;
        }
    }

    public static int GetUpgrade(UpgradeController.upgradeID id)
    {
        switch (id)
        {
            case UpgradeController.upgradeID.MAX_LIVE:
                return PlayerPrefs.GetInt(MAX_LIVE_UPGRADE);
            case UpgradeController.upgradeID.MACHINE_GUN_1:
                return PlayerPrefs.GetInt(MG_UPGRADE_1);
            case UpgradeController.upgradeID.MACHINE_GUN_2:
                return PlayerPrefs.GetInt(MG_UPGRADE_2);
            case UpgradeController.upgradeID.PHASER:
                return PlayerPrefs.GetInt(PHASER_UPGRADE);
            case UpgradeController.upgradeID.ROCKET:
                return PlayerPrefs.GetInt(ROCKET_UPGRADE);
            case UpgradeController.upgradeID.PHASER_AMMO:
                return PlayerPrefs.GetInt(PHASER_AMMO);
            case UpgradeController.upgradeID.ROCKET_AMMO:
                return PlayerPrefs.GetInt(ROCKET_AMMO);
            default:
                return 0;
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY, 0);
    }

    public static int GetCurrentLife()
    {
        return PlayerPrefs.GetInt(PLAYER_CURRENT_LIFES);
    }

    public static int GetMaxLife()
    {
        return PlayerPrefs.GetInt(PLAYER_MAX_LIFES);
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(CURRENT_COINS);
    }
}
