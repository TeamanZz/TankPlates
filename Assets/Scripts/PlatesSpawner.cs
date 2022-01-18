using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesSpawner : MonoBehaviour
{
    public static PlatesSpawner Instance;

    [SerializeField] private bool needSpawnMixedPlates;
    [SerializeField] private int mixedPlatesSpawnRequires;
    [SerializeField] private int currentMixedPlatesSpawned;

    [Space]
    [SerializeField] private int startPlateLinesCount;
    [SerializeField] private float startPlateLineZPos;
    private float lastPlateLineZPos;

    [Space]
    [SerializeField] private GameObject plateLinePrefab;
    [SerializeField] private Transform plateLinesContainer;

    [Space]
    [SerializeField] private List<int> platesValues = new List<int>();
    [SerializeField] private int currentPlateValueIndex;

    private void Awake()
    {
        Instance = this;

        lastPlateLineZPos = startPlateLineZPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (needSpawnMixedPlates)
            {
                SpawnMixedHorizontalLine(platesValues[currentPlateValueIndex - 1], platesValues[currentPlateValueIndex]);
                HandleMixedLinesSpawnedCount();
            }
            else
            {
                SpawnHorizontalLine();
            }
        }
    }

    private void HandleMixedLinesSpawnedCount()
    {
        currentMixedPlatesSpawned++;
        if (currentMixedPlatesSpawned >= mixedPlatesSpawnRequires)
        {
            currentMixedPlatesSpawned = 0;
            needSpawnMixedPlates = false;
        }
    }

    private void Start()
    {
        SpawnStartPlateLines();
    }

    private void SpawnStartPlateLines()
    {
        for (int i = 0; i < startPlateLinesCount / 2; i++)
        {
            SpawnEmptyHorizontalLine();
        }

        for (int i = 0; i < startPlateLinesCount / 2; i++)
        {
            SpawnHorizontalLine();
        }
    }

    public void SpawnMixedHorizontalLine(int minValue, int maxValue)
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        newLine.GetComponent<PlateLine>().SetMixedPlatesValue(minValue, maxValue);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
    }

    public void SpawnHorizontalLine()
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        newLine.GetComponent<PlateLine>().SetPlatesValue(platesValues[currentPlateValueIndex]);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
    }

    public void SpawnEmptyHorizontalLine()
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        newLine.GetComponent<PlateLine>().SetPlatesValue(0);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
    }
}