using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    public event Action<Cube> Encountered;
    public event Action<Cube> Released;

    [SerializeField] private Material _defaultMaterial;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public bool IsTouched { get; private set; } = false;

    private void Awake()
    {
        _wait = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (IsTouched)
                return;

            Encountered?.Invoke(this);
            StartDelayedDeleteion();
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

    private void StartDelayedDeleteion()
    {
        _coroutine = StartCoroutine(DelayedDeleteion());
    }

    private IEnumerator DelayedDeleteion()
    {
        yield return _wait;

        Released?.Invoke(this);
    }

    public void ClearParameters()
    {
        Renderer.material = _defaultMaterial;
        transform.rotation = Quaternion.identity;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.linearVelocity = Vector3.zero;
    }
}