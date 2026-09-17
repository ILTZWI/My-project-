using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _minСhance = 1;
    [SerializeField] private int _maxСhance = 100;
    [SerializeField, Min(1)] private int _reduction = 2;
    [SerializeField] private Painter _painter;
    [SerializeField] private MouseClicker _mouseClicker;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _mouseClicker.Detecting += OnCubeDetected;
    }

    private void OnDisable()
    {
        _mouseClicker.Detecting -= OnCubeDetected;
    }

    public void Split(Cube clickedCube)
    {
        int randomNumber = UnityEngine.Random.Range(_minСhance, _maxСhance + 1);

        if (randomNumber <= clickedCube.CurrentСhance)
        {
            int count = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            for (int i = 0; i < count; i++)
            {
                Cube cube = Instantiate(_cubePrefab, clickedCube.transform.position, Quaternion.identity);

                Vector3 newScale = clickedCube.transform.localScale / _reduction;
                cube.transform.localScale = newScale;

                float newChance = clickedCube.CurrentСhance / _reduction;
                cube.SetChanceValue(newChance);

                _painter.Paint(cube);
                _exploder.ApplyKnockback(clickedCube.transform.position, cube.Rigidbody, 1f / clickedCube.transform.localScale.x);
            }
        }
        else 
        {
            _exploder.Explode(clickedCube.transform.position,1f / clickedCube.transform.localScale.x );
        }

        Destroy(clickedCube.gameObject);
    }

    private void OnCubeDetected(Cube clickedCube)
    {
        Split(clickedCube);
    }
}