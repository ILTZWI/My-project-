using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private int _chanceMax = 100;
    private int _currentChance = 100;
    private int _reduction = 2;

    private void OnMouseDown()
    {
        Vector3 newScale = transform.localScale / 2;

        if (Divide())
        {
            _spawner.SpawnCubes(transform.position, newScale, _currentChance);
        }
        else 
        {
            Destroy(gameObject);
            _explosion.BlowUp();
        }
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
}
