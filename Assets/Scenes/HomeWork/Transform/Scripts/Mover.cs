using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 velocity = _speed * transform.forward;
        _rigidbody.linearVelocity = new Vector3(velocity.x,_rigidbody.linearVelocity.y,velocity.z);
    }
}
