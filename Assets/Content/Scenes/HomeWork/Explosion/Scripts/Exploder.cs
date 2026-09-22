using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _explosionEffect;

    public void Explode(Vector3 position, float multiplier = 1)
    {
        foreach (Rigidbody exploadableObject in GetExplodableObjects(position))
        {
            ApplyKnockback(position, exploadableObject, multiplier);
        }


        ParticleSystem explode = Instantiate(_explosionEffect, position, Quaternion.identity);
        explode.transform.localScale = Vector3.one * multiplier;
    }

    public void ApplyKnockback(Vector3 position, Rigidbody rigidbody, float multiplier = 1)
    {
        rigidbody.AddExplosionForce(_explosionForce * multiplier, position, _explosionRadius * multiplier);
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}