using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action Clicked;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Clicked?.Invoke();
        }
    }
}
