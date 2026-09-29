using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _lifeTime;

    public event Action<Enemy> Released;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private float _direction;

    private bool _isTouched = false;

    private void Awake()
    {
        _wait = new WaitForSeconds(_lifeTime);
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Rotate();
        Move();
        StartDelayedDeletion();
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    public void ClearParameters()
    {
        _isTouched = false;

        transform.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    public void StartDelayedDeletion()
    {
        _coroutine = StartCoroutine(DelayedDeleteion());
    }

    public void StopDelayedDeleteion()
    {
        StopCoroutine(DelayedDeleteion());
    }

    private void Deleteion()
    {
        if (_isTouched)
            return;

        StartDelayedDeletion();
        _isTouched = true;
    }

    private void Move()
    {
        transform.position += transform.forward * _runSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, _direction, 0);
    }

    private IEnumerator DelayedDeleteion()
    {
        yield return _wait;

        Released?.Invoke(this);
    }
}
