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

    public Color GetNewPlateColor(int plateValue)
    {
        if (plateValue <= 0)
        {
            return platesColors[0];
        }

        if (plateValue <= 6)
        {
            return platesColors[1];
        }


        return platesColors[0];
    }
}
