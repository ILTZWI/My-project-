using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    [SerializeField] private Material _defaultMaterial;

    public bool IsTouched { get; private set; }
    public event Action<Cube> Encountered;
    public event Action<Cube> Released;
    public Renderer Renderer { get; private set; }

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));
        Renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (IsTouched)
                return;

            Encountered?.Invoke(this);
            Activate();
            StartDelayedDeleteion();
        }
    }
    
    private void Activate()
    {
        IsTouched = true;
    }

    private void Deactivate()
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

    private void ClearParameters()
    {
        Renderer.material = _defaultMaterial;
        transform.position = Vector3.zero;
        Deactivate();
    }
}