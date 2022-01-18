using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateLine : MonoBehaviour
{
    [SerializeField] private List<Plate> platesList = new List<Plate>();

    private int platesCount = 7;

    public void DecreasePlatesCount()
    {
        platesCount--;
        if (platesCount <= 0)
        {
            PlatesSpawner.Instance.RemoveElementFromArray(this);
        }
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