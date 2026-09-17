using UnityEngine;

public class Growth : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        Grow();
    }

    private void Grow()
    {
        transform.localScale += Vector3.one * _speed * Time.deltaTime;
    }
}
