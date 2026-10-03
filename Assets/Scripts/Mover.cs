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
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private float _delay = 15;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartReturnPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _rigidbody.MoveRotation(lookRotation);

        _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Force);
    }

    private void StartReturnPosition()
    {
        _coroutine = StartCoroutine(ChangePositionDelayed());
    }

    private void ReturnStartPosition()
    {
        transform.position = _startPosition.position;
    }

    private IEnumerator ChangePositionDelayed()
    {
        bool isRun = true;

        while (isRun)
        {
            yield return _wait;

            ReturnStartPosition();
        }
    }
}
