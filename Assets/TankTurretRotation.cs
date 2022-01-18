using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretRotation : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private int degreesPerSecond;
    [SerializeField] private int rotateAngle = 45;

    private int directionCoefficient = 1;

    private void Update()
    {
        RotateTurretAutomaticaly();
    }

    private void RotateTurretAutomaticaly()
    {
        turret.Rotate(new Vector3(0, degreesPerSecond * directionCoefficient, 0) * Time.deltaTime);

        float angle = turret.localRotation.eulerAngles.y;
        angle = (angle > 180) ? angle - 360 : angle;

        if (Mathf.Abs(angle) >= rotateAngle)
            directionCoefficient *= -1;
    }
}