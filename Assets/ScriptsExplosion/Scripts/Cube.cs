using UnityEngine;
using UnityEngine.Rendering;

public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private Renderer _renderer;

    private int _chanceMax = 100;
    private int _currentChance = 100;
    private int _reduction = 2;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer == null)
            return;
    }

    private void OnMouseDown()
    {
        Vector3 newScale = transform.localScale / 2;

        if (Divide())
        {
            _spawner.SpawnCubes(transform.position, newScale, _currentChance);
        }
        else 
        {
            _explosion.BlowUp();
        }

            Destroy(gameObject);
    }

    private bool Divide()
    {
        int randomNumber = Random.Range(1, _chanceMax + 1);

        if (randomNumber <= _currentChance)
        {
            _currentChance = _currentChance / _reduction;
            return true;
        }
       
        return false;
    }

    public void SetChance(int chance)
    {
        _currentChance = chance;
    }

    public void SetMaterial(Material material)
    {
        if (material == null)
            return;

        _renderer.material = material;
    }
}
