using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{

    public static string ScoreToText(double score)
    {
        string text = "";

        if (score > 1000000000) text = (score / 1000000000).ToString("F") + "B";
        else if (score > 1000000) text = (score / 1000000).ToString("F") + "M";
        else if (score > 1000) text = (score / 1000).ToString("F") + "K";
        else if (score <= 1000) text = score.ToString("F");

        return text;
    }
}
