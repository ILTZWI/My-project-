using UnityEngine;
using System.Collections.Generic;

public class Painter : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private List<Material> _materials = new List<Material>();


    public void Paint(Cube cube)
    {
        int randomIndex = Random.Range(0, _materials.Count);

        Material material = _materials[randomIndex];

        cube.Renderer.material = material;
    }

    public void ClearMaterial(Cube cube)
    {
        Debug.Log("Домой");
        cube.Renderer.material = _defaultMaterial;
    }
}
