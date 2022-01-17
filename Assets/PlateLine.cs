using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateLine : MonoBehaviour
{
    [SerializeField] private List<Plate> platesList = new List<Plate>();

    public void SetPlatesValue(int value = 0)
    {
        for (int i = 0; i < platesList.Count; i++)
        {
            platesList[i].SetNewValue(value);
        }
    }
}