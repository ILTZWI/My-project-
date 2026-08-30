using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Logger : MonoBehaviour
{
    private void Start()
    {
        var vector = new Vector3(0.1f, 0.1f, 0.1f);
        var position = vector.normalized;
        Debug.Log(position.x);
    }
}
