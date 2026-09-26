using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;


public class SpawnerCube : MonoBehaviour
{
    private const int DefaultCapacity = 3;
    private const int MaxPoolsize = 5; 

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<GameObject> _points;
    [SerializeField] private Painter _painter;

    private CubePool _cubePool;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
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
        InvokeRepeating(nameof(SpawnCube), 0f, 0.9f);
    }
   
    private void SpawnCube()
    {
        Cube cube = _pool.Get();
        cube.Encountered += OnEncountered;
        cube.Released += OnReleased;

    }

    private void OnEncountered(Cube cube)
    {
        cube.Encountered -= OnEncountered;
        _painter.Paint(cube);
    }

    private void OnReleased(Cube cube)
    {
        ReleaseCube(cube);
    }

    private void ReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void GetCube(Cube cube)
    {
        Vector3 spawnPosition = _points[UnityEngine.Random.Range(0, _points.Count)].transform.position;
        cube.transform.position = spawnPosition;

        cube.gameObject.SetActive(true);
    }
}
