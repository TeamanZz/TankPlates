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
    [SerializeField] private int currentPlateValueIndex;
    [SerializeField] private List<int> platesValues = new List<int>();

    [SerializeField] private List<PlateLine> plateLinesList = new List<PlateLine>();


    [SerializeField] private TankMovement tankMovement;
    private void Awake()
    {
        Instance = this;

        lastPlateLineZPos = startPlateLineZPos;
    }

    public void RemoveElementFromArray(PlateLine line)
    {
        plateLinesList.Remove(line);

        tankMovement.CheckOnMoveForward(plateLinesList[0].transform.position);
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

    public void SpawnEmptyHorizontalLine()
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        newLine.GetComponent<PlateLine>().SetEmptyPlates();
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
    }

    public void SpawnHorizontalLine()
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        var plateLineComponent = newLine.GetComponent<PlateLine>();
        plateLineComponent.SetPlatesValue(platesValues[currentPlateValueIndex]);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
        plateLinesList.Add(plateLineComponent);
    }

    public void SpawnMixedHorizontalLine(int minValue, int maxValue)
    {
        Vector3 newLinePosition = new Vector3(0, 0, lastPlateLineZPos);
        var newLine = Instantiate(plateLinePrefab, newLinePosition, Quaternion.identity);
        var plateLineComponent = newLine.GetComponent<PlateLine>();
        plateLineComponent.SetMixedPlatesValue(minValue, maxValue);
        newLine.transform.SetParent(plateLinesContainer);
        lastPlateLineZPos += 2;
        plateLinesList.Add(plateLineComponent);
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

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         if (needSpawnMixedPlates)
    //         {
    //             SpawnMixedHorizontalLine(platesValues[currentPlateValueIndex - 1], platesValues[currentPlateValueIndex]);
    //             HandleMixedLinesSpawnedCount();
    //         }
    //         else
    //         {
    //             SpawnHorizontalLine();
    //         }
    //     }
    // }

}