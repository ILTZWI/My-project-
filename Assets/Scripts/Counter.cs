using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private int _count = 0;
    private bool _isCounting = false;
    private Coroutine _coroutine;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_isCounting)
            {
                StopCounter();
            }
            else 
            {
                StartCounter();
            }
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
            yield return new WaitForSeconds(0.5f);
            _count++;
            Debug.Log("Счетчик :" + _count);
        }
    }
}
