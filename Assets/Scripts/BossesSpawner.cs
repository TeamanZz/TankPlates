using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesSpawner : MonoBehaviour
{
    public static BossesSpawner Instance;

    public GameObject lastSpawnedBoss;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private List<GameObject> bossPrefabs = new List<GameObject>();

    public void CheckOnBossSpawn(float lastPlateLineZPos)
    {
        var newTank = Instantiate(bossPrefabs[0], new Vector3(0, 2, lastPlateLineZPos), Quaternion.identity);
    }

    public void SpawnBoss()
    {
        lastSpawnedBoss = Instantiate(bossPrefabs[0], new Vector3(0, 2, PlatesSpawner.Instance.lastPlateLineZPos), Quaternion.Euler(0, 180, 0));

        lastSpawnedBoss.GetComponent<BossTurret>().projectileDamage = Mathf.Abs((int)ProgressController.Instance.damageLvl / 1.5f);
        lastSpawnedBoss.GetComponent<BossTurret>().totalHp = ProgressController.Instance.damageLvl * 20;
    }
}