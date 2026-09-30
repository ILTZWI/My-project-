using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Enemy _prefabEnemy;

    [SerializeField] private float _delay;
    [SerializeField] private float _direction;


    private Transform _spawnPosition;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private bool _isRun = true;

    private int _defaultCapacity = 4;
    private int _maxSize = 10;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);

        _enemyPool = new ObjectPool<Enemy>
        (
             createFunc: () => Instantiate(_prefabEnemy),
            actionOnGet: (enemy) => Spawn(enemy),
            actionOnRelease: (enemy) => Destroy(enemy),
            actionOnDestroy: (enemy) => Object.Destroy(enemy),
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    private void Start()
    {
        _enemyPool.Get();
        StartSpawn();
    }

    private void Spawn(Enemy enemy)
    {
        enemy.transform.position = _spawnPosition.position;

        enemy.Destroyed += OnDestroyed;
        enemy.SetDirection(_direction);
        enemy.gameObject.SetActive(true);
    }
    private void Destroy(Enemy enemy)
    {
        enemy.Destroyed -= OnDestroyed;
        enemy.ClearParameters();
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyed(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void StartSpawn()
    {
        _coroutine = StartCoroutine(EnemySpawnDelayed());
    }

    private IEnumerator EnemySpawnDelayed()
    {
        while (_isRun)
        {
            yield return _wait;
            _enemyPool.Get();
        }
    }
}