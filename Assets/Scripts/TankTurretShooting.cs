using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretShooting : MonoBehaviour
{
    [SerializeField] private Transform tankOrigin;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private float delayBetweenShot = 1;

    private float currentTimerValue;

    private void Awake()
    {
        currentTimerValue = delayBetweenShot;
    }

    private void Update()
    {
        currentTimerValue -= Time.deltaTime;
        if (currentTimerValue <= 0)
        {
            var newBullet = Instantiate(projectilePrefab, shootPoint.position, transform.GetChild(1).localRotation);
            // var newParticles = Instantiate(shootParticles, shootPoint.position, transform.GetChild(1).localRotation);
            var forceVector = shootPoint.position - tankOrigin.position;
            newBullet.GetComponent<Rigidbody>().AddForce(forceVector.normalized * 5, ForceMode.Impulse);
            currentTimerValue = delayBetweenShot;
        }
    }
}