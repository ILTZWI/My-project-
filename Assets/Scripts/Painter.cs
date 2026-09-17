using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    public void Paint(Cube cube)
    {
        int randomIndex = Random.Range(0, _materials.Count);

        Material material = _materials[randomIndex];

        cube.Renderer.material = material;
    }
}