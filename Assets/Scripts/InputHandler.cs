using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked?.Invoke();
        }
    }
}