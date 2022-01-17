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

    public Color GetNewPlateColor(int state)
    {
        return platesColors[state];
    }

    public Color GetLerpedColor(int value)
    {
        return new Color();
    }
}