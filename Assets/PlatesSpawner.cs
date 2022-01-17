using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesSpawner : MonoBehaviour
{
    [SerializeField] private int startPlateLinesCount;
    [SerializeField] private float startPlateLineZPos;
    private float lastPlateLineZPos;

    [SerializeField] private GameObject plateLinePrefab;
    [SerializeField] private Transform plateLinesContainer;

    private void Awake()
    {
        lastPlateLineZPos = startPlateLineZPos;
    }

    private void Start()
    {
        SpawnStartPlateLines();
    }

    private void SpawnStartPlateLines()
    {
        for (int i = 0; i < startPlateLinesCount / 2; i++)
        {
            SpawnLine();
        }

        for (int i = 0; i < startPlateLinesCount / 2; i++)
        {
            SpawnLine(1);
        }
    }

    private void SpawnLine(int platesValue = 0)
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        newLine.GetComponent<PlateLine>().SetPlatesValue(platesValue);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
    }
}