using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementDetection : MonoBehaviour
{
    private PlateLine plateLine;
    [SerializeField] private TankMovement tankMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlateLine>(out plateLine))
        {
            // tankMovement.needMoveForward = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlateLine>(out plateLine))
        {
            // tankMovement.needMoveForward = true;
        }
    }
}