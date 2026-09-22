using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<GameObject> _points;
    [SerializeField] private Platform _platform;
    [SerializeField] private Painter _painter;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => GetCube(cube),
            actionOnRelease: (cube) => ReleaseCube(cube),
            actionOnDestroy: (cube) => Destroy(cube)
        );
    }

    private void OnEnable()
    {
        _platform.Encountered += OnEncountered;
    }

    private void OnDisable()
    {
        _platform.Encountered -= OnEncountered;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0f, 0.9f);
    }
    
    private void OnEncountered(Cube cube)
    {
        _painter.Paint(cube);
    }

    private void SpawnCube()
    {
        _pool.Get();
    }

    private void ReleaseCube(Cube cube)
    {
        _painter.ClearMaterial(cube);
        cube.Deactivate();
        cube.gameObject.SetActive(false);
    }

    private void GetCube(Cube cube)
    {
        Vector3 spawnPosition = _points[UnityEngine.Random.Range(0, _points.Count)].transform.position;
        cube.transform.position = spawnPosition;

        Cube scriptCube = cube.GetComponent<Cube>();
        scriptCube.SetPool(_pool);

        cube.gameObject.SetActive(true);
    }
}