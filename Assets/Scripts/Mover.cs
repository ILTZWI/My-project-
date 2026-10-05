using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _startPosition;

    private Rigidbody _rigidbody;

    private bool _isHeadingHome = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        Transform target;

        if (_isHeadingHome)
        {
            target = _target;
        }
        else
        {
            target = _startPosition;
        }

        Move(target);

        if (CompareDistance(target) < 1)
            _isHeadingHome = _isHeadingHome ? false : true;
    }

    private void ClearSpeed()
    {
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void Move(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _rigidbody.MoveRotation(lookRotation);

        _rigidbody.linearVelocity = direction * _moveSpeed;
    }

    private float CompareDistance(Transform target)
    {
        float distance = (transform.position - target.position).sqrMagnitude;
        return distance;
    }
}
