using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsHandler : MonoBehaviour
{
    public static ColorsHandler Instance;

    [SerializeField] private List<Color> platesColors = new List<Color>();
    [SerializeField] private List<Color> textColors = new List<Color>();

    private void Awake()
    {
        Instance = this;
    }

    // public Color GetNewPlateColor(int state)
    // {
    //     return platesColors[state];
    // }

    public Color GetLerpedColor(int value)
    {
        var truncNumber = Math.Truncate((double)(value / 6));
        if (truncNumber <= 1)
            truncNumber++;
        if ((int)truncNumber >= platesColors.Count)
        {
            return platesColors[UnityEngine.Random.Range(1, platesColors.Count)];
        }
        else
        {
            return platesColors[(int)truncNumber];
        }
    }

    public Color GetTextColor(int value)
    {
        var truncNumber = Math.Truncate((double)(value / 6));
        if (truncNumber <= 1)
            truncNumber++;
        if ((int)truncNumber >= textColors.Count)
        {
            return textColors[UnityEngine.Random.Range(1, textColors.Count)];
        }
        else
        {
            return textColors[(int)truncNumber];
        }
    }

    public Color GetDefaultColor()
    {
        return platesColors[0];
    }
}