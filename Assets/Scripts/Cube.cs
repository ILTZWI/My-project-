using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private ObjectPool<Cube> _pool;

    public bool IsTouched { get; private set; }
    public event Action<Cube> Encountered;
    public Renderer Renderer { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (IsTouched)
                return;

            Encountered?.Invoke(this);
        }
    }
    
    public void Activate()
    {
        IsTouched = true;
    }

    public void Deactivate()
    {
        IsTouched = false;
    }
}