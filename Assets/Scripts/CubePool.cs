using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<GameObject> _points;

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

    public void SpawnCube()
    {
        _pool.Get();
    }

    public void ReleaseCube(Cube cube)
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