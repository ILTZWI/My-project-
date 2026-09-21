using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputHandler _handler;

    public Action<Cube> Detecting;

    private void OnEnable()
    {
        _handler.Clicked += OnClickMouseDown;
    }

    private void OnDisable()
    {
        _handler.Clicked -= OnClickMouseDown;
    }

    public void OnClickMouseDown(Vector3 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if(IsHitCube(ray,out Cube cube))
            Detecting?.Invoke(cube);
    }

    public bool IsHitCube(Ray ray, out Cube cube)
    {
        cube = default;
        
        if(Physics.Raycast(ray, out RaycastHit hit) == false)
            return false;

        return hit.collider.gameObject.TryGetComponent(out cube);
    }
}