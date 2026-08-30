using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _countInterval = 0.05f;

    private int _count = 0;
    private bool _isCounting = false;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    public event Action<int> CountChanged;

    private void Awake()
    {
        _wait = new WaitForSeconds(_countInterval);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isCounting)
                StopCounter();
            else
                StartCounter();
        }
    }

    private void StartCounter()
    {
        _isCounting = true;
        _coroutine = StartCoroutine(CountCoroutine());
    }

    private void StopCounter()
    {
        _isCounting = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }


    private IEnumerator CountCoroutine()
    {
        while (_isCounting)
        {
            _count++;
            yield return _wait;

            CountChanged?.Invoke(_count);
        }
    }
}
