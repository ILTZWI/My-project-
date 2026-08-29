using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var nextPostion = transform.position;
        nextPostion.x += _speed * Time.deltaTime;
        transform.position = nextPostion;
    }
}
