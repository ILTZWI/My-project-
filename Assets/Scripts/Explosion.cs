using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] [Range(1, 1000)] private float _baseExplosionRadius;
    [SerializeField][Range(1, 1000)] private float _baseExplosionForce;
    [SerializeField] private ParticleSystem _explosionEffect;

    public void BlowUp()
    {
        Explode();
        Instantiate(_explosionEffect, transform.position, transform.rotation);
    }

    public void Explode()
    {
        float size = transform.localScale.x;

        float explosionRadius = _baseExplosionRadius / size;
        float explosionForce = _baseExplosionForce / size;

        foreach (Rigidbody exploadableObject in GetExplodableObjects(explosionRadius))
        {
            float distance = Vector3.Distance(transform.position, exploadableObject.position);
            float forceMultiplier = 1 - distance / explosionRadius;

            exploadableObject.AddExplosionForce(explosionForce * forceMultiplier, transform.position, explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
