using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private ObjectPool<Cube> _pool;
    private const int _minDelay = 2;
    private const int _maxDelay = 5;


    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    public bool IsTouched { get; private set; }
    public Renderer Renderer { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay));

        Renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (IsTouched)
                return;

            StartDelayedDelteion();
        }
    }

    private void StartDelayedDelteion()
    {
        _coroutine = StartCoroutine(DelayedDeletion());
    }

    private IEnumerator DelayedDeletion()
    {
        yield return _wait;
        _pool.Release(this);
    }

    public void Activate()
    {
        IsTouched = true;
    }

    public void Deactivate()
    {
        IsTouched = false;
    }

    public void SetPool(ObjectPool<Cube> pool)
    {
        _pool = pool;
    }
}
