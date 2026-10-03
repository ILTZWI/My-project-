using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _startPosition;

    private Rigidbody _rigidbody;

    private float _delay = 15;

    private bool _isLocatedHome = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (_isLocatedHome)
        {
            Move(_startPosition);
            if (Vector3.Distance(_startPosition.position,transform.position) < 1f)
            {
                _isLocatedHome = false;
            }
        }
        else
        {
            Vector3 direction = (_target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _rigidbody.MoveRotation(lookRotation);

            _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Force);

            if (Vector3.Distance(_target.position, transform.position) < 1f)
            {
                _isLocatedHome = true;
                ClearSpeed();
            }
        }
    }

    private void ClearSpeed()
    {
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void Move(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Force);
    }

    private void ReturnStartPosition()
    {
        Vector3 direction = (_startPosition.position - transform.position).normalized;

        _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Force);
    }
}
