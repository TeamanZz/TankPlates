using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankProjectile : MonoBehaviour
{
    public int damage;
    private Plate plate;
    private ReflectionSurface surface;
    private Rigidbody rb;

    Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Plate>(out plate))
        {
            if (!plate.enabled)
                return;
            plate.TakeDamage(damage);
            Destroy(gameObject);
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
}