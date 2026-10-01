using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform _destination;
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private float _delay;

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
            actionOnGet: (enemy) => GetEnemy(enemy),
            actionOnRelease: (enemy) => Destroy(enemy),
            actionOnDestroy: (enemy) => Object.Destroy(enemy),
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    private void Start()
    {
        StartSpawn();
    }

    private void GetEnemy(Enemy enemy)
    {
        enemy.transform.position = _spawnPosition.position;

        enemy.Destination(_destination);
        enemy.Destroyed += OnDestroyed;
        enemy.gameObject.SetActive(true);
    }

    private void Spawn()
    {
        foreach (Transform position in _spawnPositions)
        {
            _spawnPosition = position;
            _enemyPool.Get();
        }
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
            Spawn();
        }
    }
}