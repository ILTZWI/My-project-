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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            if (_isTouched)
                return;

            StartDeletion();
            _isTouched = true;
        }
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

    private void StartDeletion()
    {
        _coroutine = StartCoroutine(DelayedDeleteion());
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
