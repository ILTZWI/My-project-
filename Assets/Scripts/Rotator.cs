using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _rotationCentr;
    

    private void Update ()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up,_speed * Time.deltaTime);
        transform.RotateAround(_rotationCentr.position, Vector3.up, _speed * Time.deltaTime);
    }
}
