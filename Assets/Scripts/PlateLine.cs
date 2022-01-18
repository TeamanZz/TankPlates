using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateLine : MonoBehaviour
{
    [SerializeField] private List<Plate> platesList = new List<Plate>();

    private int platesCount;

    public void DecreasePlatesCount()
    {
        platesCount--;
    }

    public void SetPlatesValue(int value = 0)
    {
        for (int i = 0; i < platesList.Count; i++)
        {
            platesList[i].SetNewNonZeroValue(value);
        }
    }

    public void SetMixedPlatesValue(int minValue, int MaxValue)
    {
        for (int i = 0; i < platesList.Count; i++)
        {
            platesList[i].SetNewNonZeroValue(Random.Range(minValue, MaxValue + 1));
        }
    }

    public void SetEmptyPlates()
    {
        for (int i = 0; i < platesList.Count; i++)
        {
            platesList[i].SetPlateAsEmptyOnStart();
        }
    }
}