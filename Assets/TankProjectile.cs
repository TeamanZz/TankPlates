using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankProjectile : MonoBehaviour
{
    public int damage;
    private Plate plate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Plate>(out plate))
        {
            if (!plate.enabled)
                return;
            plate.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
