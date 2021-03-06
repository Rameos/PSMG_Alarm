﻿using UnityEngine;
using System.Collections;
using System;

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

    // store highscore
    private static readonly string HIGHSCORE_SCORES = "highscore_scores";
    private static readonly string HIGHSCORE_NAMES = "highscore_names";

    // store player life stats
    private static readonly string PLAYER_CURRENT_LIFES = "player_current_lifes";
    private static readonly string PLAYER_MAX_LIFES = "player_max_lifes";

    // store settings
    private static readonly string SOUND_VOLUME = "sound_volume";
    private static readonly string MUSIC_VOLUME = "music_volume";
    private static readonly string USE_GAZE = "use_gaze";

    // store bought upgrades
    private static readonly string MG_UPGRADE_1 = "mg_upgrade_1";
    private static readonly string MG_UPGRADE_2 = "mg_upgrade_2";
    private static readonly string PHASER_UPGRADE = "phaser_upgrade";
    private static readonly string ROCKET_UPGRADE = "rocket_upgrade";
    private static readonly string PHASER_AMMO = "phaser_ammo";
    private static readonly string ROCKET_AMMO = "rocket_ammo";
    private static readonly string MAX_LIVE_UPGRADE1 = "max_live_upgrade1";
    private static readonly string MAX_LIVE_UPGRADE2 = "max_live_upgrade2";
    private static readonly string X_RAY_UPGRADE = "x_ray_upgrade";
    private static readonly string TURBO_UPGRADE = "turbo_upgrade";
    private static readonly string SHIELD_UPGRADE = "shield_upgrade";

    public static void Reset()
    {
        bool control = GetControl();
        HighscoreElement[] highscore = GetHighscore();

        PlayerPrefs.DeleteAll();

        SetControl(control);
        SetHighscore(highscore);
    }

    public static void SetHighscore(HighscoreElement[] score)
    {
        int[] scores = new int[score.Length];
        string[] names = new string[score.Length];
        int i = 0;

        foreach (HighscoreElement currentScore in score)
        {
            scores[i] = currentScore.GetScore();
            names[i] = currentScore.GetName();
            i++;
        }

        PlayerPrefsX.SetIntArray(HIGHSCORE_SCORES, scores);
        PlayerPrefsX.SetStringArray(HIGHSCORE_NAMES, names);
    }

    public static void SetSound(float value)
    {
        PlayerPrefs.SetFloat(SOUND_VOLUME, value);
    }

    public static void SetMusic(float value)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, value);
    }

    public static void SetControl(bool value)
    {
        PlayerPrefs.SetInt(USE_GAZE, Convert.ToInt32(value));
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

    public static void SetLevel(string value)
    {
        PlayerPrefs.SetString(CURRENT_LEVEL, value);
    }

    public static void SetUpgrade(int value, UpgradeController.upgradeID id)
    {
        switch (id)
        {
            case UpgradeController.upgradeID.MAX_LIVE1:
                PlayerPrefs.SetInt(MAX_LIVE_UPGRADE1, value);
                SetCurrentLive(GetCurrentLife() + 1);
                SetMaxLives(GetMaxLife() + 1);
                break;
            case UpgradeController.upgradeID.MAX_LIVE2:
                PlayerPrefs.SetInt(MAX_LIVE_UPGRADE2, value);
                SetCurrentLive(GetCurrentLife() + 1);
                SetMaxLives(GetMaxLife() + 1);
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
            case UpgradeController.upgradeID.X_RAY:
                PlayerPrefs.SetInt(X_RAY_UPGRADE, value);
                break;
            case UpgradeController.upgradeID.TURBO:
                PlayerPrefs.SetInt(TURBO_UPGRADE, value);
                break;
            case UpgradeController.upgradeID.SHIELD:
                PlayerPrefs.SetInt(SHIELD_UPGRADE, value);
                break;
        }
    }

    public static int GetUpgrade(UpgradeController.upgradeID id)
    {
        switch (id)
        {
            case UpgradeController.upgradeID.MAX_LIVE1:
                return PlayerPrefs.GetInt(MAX_LIVE_UPGRADE1);
            case UpgradeController.upgradeID.MAX_LIVE2:
                return PlayerPrefs.GetInt(MAX_LIVE_UPGRADE2);
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
            case UpgradeController.upgradeID.TURBO:
                return PlayerPrefs.GetInt(TURBO_UPGRADE);
            case UpgradeController.upgradeID.X_RAY:
                return PlayerPrefs.GetInt(X_RAY_UPGRADE);
            case UpgradeController.upgradeID.SHIELD:
                return PlayerPrefs.GetInt(SHIELD_UPGRADE);
            default:
                return 0;
        }
    }

    public static float GetSound()
    {
        return PlayerPrefs.GetFloat(SOUND_VOLUME, 1f);
    }

    public static float GetMusic()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f);
    }

    public static HighscoreElement[] GetHighscore()
    {
        int[] scores = PlayerPrefsX.GetIntArray(HIGHSCORE_SCORES, 0, 10);
        string[] names = PlayerPrefsX.GetStringArray(HIGHSCORE_NAMES, "-- no player --", 10);
        HighscoreElement[] highscores = new HighscoreElement[scores.Length];

        for (int i = 0; i < highscores.Length; i++)
        {
            highscores[i] = new HighscoreElement(names[i], scores[i]);
        }
        return highscores;
    }

    public static bool GetControl()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(USE_GAZE, 0));
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

    public static int GetScore()
    {
        return PlayerPrefs.GetInt(CURRENT_SCORE);
    }

    public static string GetLevel()
    {
        return PlayerPrefs.GetString(CURRENT_LEVEL, "submarine2");
    }
}
