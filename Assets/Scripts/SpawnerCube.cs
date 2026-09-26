using System.Collections;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    [SerializeField] private CubePool _cubePool;
    [SerializeField] private Painter _painter;
    [SerializeField] private Cube _cube;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0f, 0.9f);
    }

    private void OnEnable()
    {
        _cube.Encountered += OnEncountered;
    }

    private void OnDisable()
    {
        _cube.Encountered -= OnEncountered;
    }

    private void StartDelayedDeleteion()
    {
        _coroutine = StartCoroutine(DelayedDeletion());
    }

    private void SpawnCube()
    {
        _cubePool.SpawnCube();
    }

    private IEnumerator DelayedDeletion()
    {
        yield return _wait;

        OnReleased(_cube);
    }

    private void OnEncountered(Cube cube)
    {
        _painter.Paint(cube);
        cube.Activate();
        StartDelayedDeleteion();
    }
    private void OnReleased(Cube cube)
    {
        _painter.ClearMaterial(cube);
        cube.Deactivate();
        _cubePool.ReleaseCube(cube);
    }
}
