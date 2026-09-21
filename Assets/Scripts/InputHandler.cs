using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector3> Clicked;

    private int Button = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(Button))
        {
            Clicked?.Invoke(Input.mousePosition);
        }
    }
}