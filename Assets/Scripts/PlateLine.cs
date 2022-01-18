using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateLine : MonoBehaviour
{
    [SerializeField] private List<Plate> platesList = new List<Plate>();

    public void SetPlatesValue(int value = 0)
    {
        if (value == 0)
        {
            for (int i = 0; i < platesList.Count; i++)
            {
                platesList[i].SetEmptyValues();
            }
        }
        else
        {
            for (int i = 0; i < platesList.Count; i++)
            {
                platesList[i].SetNewValue(value);
            }
        }
    }

    public void SetMixedPlatesValue(int minValue, int MaxValue)
    {
        for (int i = 0; i < platesList.Count; i++)
        {
            platesList[i].SetNewValue(Random.Range(minValue, MaxValue + 1));
        }
    }
}