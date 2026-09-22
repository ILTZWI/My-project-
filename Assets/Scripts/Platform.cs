using System;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Painter _painter;

    public event Action<Cube> Encountered;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.IsTouched)
                return;
            
            Encountered?.Invoke(cube);  
            cube.Activate();
        }
    }
}
