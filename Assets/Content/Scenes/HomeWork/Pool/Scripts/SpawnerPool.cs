using UnityEngine;
using UnityEngine.Pool;

public class SpawnerPool: MonoBehaviour
{
    [SerializeField] private GameObject _spherePrefab;
    [SerializeField] private Transform _startPoint;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_spherePrefab),
            actionOnGet: (sphere) => GetSphere(sphere),
            actionOnRelease: (sphere) => sphere.SetActive(false),
            actionOnDestroy: (sphere) => Destroy(sphere)
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSphere), 0f, 3f);
    }

    private void SpawnSphere()
    {
        _pool.Get();
    }

    private void GetSphere(GameObject sphere)
    {
        sphere.transform.position = _startPoint.position;

        Sphere sphereScript = sphere.GetComponent<Sphere>();
        sphereScript.SetPool(_pool);

        sphere.SetActive(true);
    }

    
}