using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private Enemy _prefabEnemy;

    [SerializeField] private float _delay;
    [SerializeField] private float _direction;

    private ObjectPool<Enemy> _enemyPool;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private bool _isRun = true;

    private int _defaultCapacity = 4;
    private int _maxSize = 10;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);

        _enemyPool = new ObjectPool<Enemy>
            (
            createFunc: () => Instantiate(_prefabEnemy),
            actionOnGet: (enemy) => GetEnemy(enemy),
            actionOnRelease: (enemy) => Release(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy),
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
        enemy.Released += OnReleased;
        enemy.SetDirection(_direction);
        enemy.transform.position = _spawnPosition.transform.position;

        enemy.gameObject.SetActive(true);
    }

    private void OnReleased(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void Release(Enemy enemy)
    {
        enemy.Released -= OnReleased;
        enemy.ClearParameters();
        enemy.gameObject.SetActive(false);
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