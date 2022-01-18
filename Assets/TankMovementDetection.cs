using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementDetection : MonoBehaviour
{
    private PlateLine plateLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlateLine>(out plateLine))
        {

        }

    }
}
