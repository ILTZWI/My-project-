using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube_1 _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _minСhance = 1;
    [SerializeField] private int _maxСhance = 100;
    [SerializeField, Min(1)] private int _reduction = 2;
    [SerializeField] private Painter_1 _painter;

    public event Action<Vector3,Cube_1> Spawned;
    public event Action<Vector3, float> BlowUped;

    public void Spawn(Cube_1 clickedCube, Vector3 cubeScale)
    {
        int randomNumber = UnityEngine.Random.Range(_minСhance, _maxСhance + 1);

        if (randomNumber <= clickedCube.CurrentСhance)
        {
            int count = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            for (int i = 0; i < count; i++)
            {
                Cube_1 cube = Instantiate(_cubePrefab, clickedCube.transform.position, Quaternion.identity);

                Vector3 newScale = cubeScale;
                cube.transform.localScale = newScale;

                float newChance = clickedCube.CurrentСhance / _reduction;
                cube.SetChanceValue(newChance);

                _painter.Paint(cube);
                Spawned?.Invoke(clickedCube.transform.position,cube);
            }
        }
        else 
        {
            BlowUped?.Invoke(clickedCube.transform.position, 1f / clickedCube.transform.localScale.x);
        }

        Destroy(clickedCube.gameObject);
    }
}