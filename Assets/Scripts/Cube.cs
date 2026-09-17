using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public float CurrentСhance = 100;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();

        if (Renderer == null)
            return;
    }
    
    public void SetChanceValue(float chance)
    {
        CurrentСhance = chance;
    }
}