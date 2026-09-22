using System.Collections.Generic;
using UnityEngine;

public class Painter_1 : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    public void Paint(Cube_1 cube)
    {
        int randomIndex = Random.Range(0, _materials.Count);

        Material material = _materials[randomIndex];

        cube.Renderer.material = material;
    }
}