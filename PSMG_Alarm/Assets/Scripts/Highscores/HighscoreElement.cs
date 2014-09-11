﻿using UnityEngine;
using System.Collections;

public class HighscoreElement
{
    private int score;
    private string name;

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public string GetName()
    {
        return name;
    }

    public int GetScore()
    {
        return score;
    }
}