using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesSpawner : MonoBehaviour
{
    public static BossesSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float tankPower;
    [SerializeField] private List<GameObject> bossPrefabs = new List<GameObject>();

    public void CheckOnBossSpawn(float lastPlateLineZPos)
    {
        var newTank = Instantiate(bossPrefabs[0], new Vector3(0, 2, lastPlateLineZPos), Quaternion.identity);
    }
}