using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class SpawnerEnemy : MonoBehaviour
{
    private const int MaxAngle = 360;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _delay;

    private bool _isCounting = false;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        _isCounting = true;
        _coroutine = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (_isCounting)
        {
            GameObject enemy = Instantiate(_enemy, transform.position, Quaternion.Euler(0, UnityEngine.Random.value * MaxAngle, 0));
            Destroy(enemy,6);

            yield return _wait;
        }
    }
}
