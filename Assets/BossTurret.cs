using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTurret : MonoBehaviour
{
    [SerializeField] private Transform tankOrigin;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private GameObject damageParticles;
    [SerializeField] private float delayBetweenShot = 1;

    [SerializeField] public float projectileDamage = 1;

    public int totalHp;
    [SerializeField] int currentHp;

    [SerializeField] Image hpBar;

    private float currentTimerValue;

    private void Awake()
    {
        currentTimerValue = delayBetweenShot;
        currentHp = totalHp;
    }

    public void TakeDamage(int value, Vector3 position)
    {
        var newParticles = Instantiate(damageParticles, position, Quaternion.identity, transform.parent);
        currentHp -= value;
        hpBar.fillAmount = (float)currentHp / (float)totalHp;
        if (currentHp <= 0)
            Destroy(gameObject);
    }

    private void Update()
    {
        currentTimerValue -= Time.deltaTime;
        if (currentTimerValue <= 0)
        {
            var newBullet = Instantiate(projectilePrefab, shootPoint.position, transform.GetChild(1).rotation);
            // var newParticles = Instantiate(shootParticles, shootPoint.position, transform.GetChild(1).localRotation);
            var forceVector = shootPoint.position - tankOrigin.position;
            newBullet.GetComponent<Rigidbody>().AddForce(forceVector.normalized * 5, ForceMode.Impulse);
            newBullet.GetComponent<BossTankProjectile>().damage = (int)projectileDamage;
            currentTimerValue = delayBetweenShot;
        }
    }
}
