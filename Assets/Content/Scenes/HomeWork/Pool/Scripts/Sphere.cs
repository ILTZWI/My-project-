using UnityEngine;
using UnityEngine.Pool;

public class Sphere : MonoBehaviour
{
    private ObjectPool<GameObject> _pool;

    private void OnCollisionEnter(Collision collision)
    {
        _pool.Release(gameObject);
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }
}
