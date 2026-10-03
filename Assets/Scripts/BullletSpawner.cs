using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting;

    private Rigidbody _rigidbody;
    private Transform _objectToShoot;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 1;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartSpawn();
    }  

    private void StartSpawn()
    {
        _coroutine = StartCoroutine(DelayedSpawnBullet());
    }

    private IEnumerator DelayedSpawnBullet()
    {
        bool isWork = true;
        while (isWork)
        {
            yield return new WaitForSeconds(_timeWaitShooting);

            var _vector3direction = (_objectToShoot.position - transform.position).normalized;
            var NewBullet = Instantiate(_prefab, transform.position + _vector3direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction;
            NewBullet.GetComponent<Rigidbody>().linearVelocity = _vector3direction * _speed;
        }
    }
}
