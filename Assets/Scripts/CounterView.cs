using System;
using Unity.VisualScripting;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private void OnEnable()
    {
        _counter.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _counter.CountChanged -= OnCountChanged;
    }

    private void OnCountChanged(int count)
    {
        Debug.Log("Счётчик: " + count);
    }
}
