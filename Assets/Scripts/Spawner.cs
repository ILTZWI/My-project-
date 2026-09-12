using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private Painter _painter;


    public void SpawnCubes(Vector3 position, Vector3 scale,int chance)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(_cubePrefab, position, Quaternion.identity);

            cube.transform.localScale = scale;

            _painter.Paint(cube);

            CubeClickHandler clickHandler = cube.GetComponent<CubeClickHandler>();

            clickHandler.SetChance(chance);
        }
    }
}
