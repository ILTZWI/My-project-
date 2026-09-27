using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    private const int DefaultCapacity = 3;
    private const int MaxPoolsize = 5;
    private const float SpawnDelayed = 0.7f;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<GameObject> _points;
    [SerializeField] private Painter _painter;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private ObjectPool<Cube> _pool;

    private bool _isCounting = true;

    private void Awake()
    {
        _wait = new WaitForSeconds(SpawnDelayed);

        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => GetCube(cube),
            actionOnRelease: (cube) => ReleaseCube(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            defaultCapacity: DefaultCapacity,
            maxSize: MaxPoolsize
        );
    }

    private void Start()
    {
        StartSpawnDelayed();
    }

    private void SpawnCube()
    {
        Cube cube = _pool.Get();
        cube.Encountered += OnEncountered;
        cube.Released += OnReleased;
    }

    private void OnEncountered(Cube cube)
    {
        _painter.Paint(cube);
        cube.Activate();
    }

    private void OnReleased(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ReleaseCube(Cube cube)
    {
        cube.Encountered -= OnEncountered;
        cube.Released -= OnReleased;
        cube.ClearParameters();
        cube.gameObject.SetActive(false);
        cube.Deactivate();
    }

    private void GetCube(Cube cube)
    {
        Vector3 spawnPosition = _points[UnityEngine.Random.Range(0, _points.Count)].transform.position;
        cube.transform.position = spawnPosition;

        cube.gameObject.SetActive(true);
    }

    private void StartSpawnDelayed()
    {
        _coroutine = StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        while (_isCounting)
        {
            yield return _wait;

            SpawnCube();
        }
    }
}
