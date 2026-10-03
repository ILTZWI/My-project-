using System;
using System.Collections;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _lifeTime;

    private Transform _destination;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private bool _isTouched = false;

    public event Action<Enemy> Destroyed;

    private void Awake()
    {
        _wait = new WaitForSeconds(_lifeTime);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        StartDelayedDeletion();
    }

    private void OnDisable()
    {
        StopDelayedDeleteion();
    }

    public void Destination(Transform transform)
    {
        _destination = transform;
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
        if (_coroutine != null)
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
        if (_destination == null)
            return;

        Vector3 direction = (_destination.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _rigidbody.MoveRotation(lookRotation);

        _rigidbody.AddForce(direction * _runSpeed, ForceMode.Force);
    }

    private IEnumerator DelayedDeleteion()
    {
        yield return _wait;

        Destroyed?.Invoke(this);
    }
}
