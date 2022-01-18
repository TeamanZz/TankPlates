using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesSpawnCollider : MonoBehaviour
{
    [SerializeField] private PlatesSpawner platesSpawner;

    private void OnTriggerEnter(Collider other)
    {
        PlateLine plateLine;
        if (other.TryGetComponent<PlateLine>(out plateLine))
        {
            platesSpawner.SpawnHorizontalLine();
        }
    }
}