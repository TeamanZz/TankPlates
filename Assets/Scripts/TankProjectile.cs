using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankProjectile : MonoBehaviour
{
    public int damage = 1;
    private Plate plate;
    private ReflectionSurface surface;
    private Rigidbody rb;

    Vector3 velocity;

    private bool canInteract = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Plate>(out plate))
        {
            if (plate.value <= 0)
                return;

            if (!canInteract)
                return;

            var damageToProjectile = plate.value;

            if (damage > 0)
                plate.TakeDamage(damage);

            Debug.Log(damageToProjectile);
            damage -= damageToProjectile;

            if (damage <= 0)
            {
                canInteract = false;
                Destroy(gameObject);
            }

        }

        BossTurret bossTurret;
        if (other.TryGetComponent<BossTurret>(out bossTurret))
        {
            var localDamage = damage;

            if (localDamage > 0)
                bossTurret.TakeDamage(localDamage, other.transform.position);

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Plate plate;
        if (other.gameObject.TryGetComponent<Plate>(out plate))
        {
            ReflectFromPlate(other);
        }

        if (other.gameObject.TryGetComponent<ReflectionSurface>(out surface))
        {
            Reflect(other);
        }
    }

    private void Reflect(Collision other)
    {
        Vector3 wallNormal = other.contacts[0].normal;
        var dir = Vector3.Reflect(velocity, wallNormal).normalized;

        rb.velocity = dir * 5;
        float arrowAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(arrowAngle, Vector3.up);
        transform.localRotation = new Quaternion(transform.localRotation.x * -1.0f,
                                        transform.localRotation.y,
                                        transform.localRotation.z,
                                        transform.localRotation.w * -1.0f);
    }

    private void ReflectFromPlate(Collision other)
    {
        Vector3 wallNormal = other.contacts[0].normal;
        var dir = Vector3.Reflect(velocity, wallNormal).normalized;

        rb.velocity = dir * 5;
        transform.localRotation = new Quaternion(transform.localRotation.x * -1.0f,
                                        transform.localRotation.y,
                                        transform.localRotation.z,
                                        transform.localRotation.w * -1.0f);
        if (dir.z < 0)
            transform.eulerAngles += new Vector3(0, 180, 0);
    }
}