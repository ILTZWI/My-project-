using System.Collections;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Signaling _signaling;
    [SerializeField] private float _rateOfChange;

    private float _signalizeTargetOn = 1;
    private float _signalizeTargetOff = 0;
    private float _violators = 0;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _signaling.Entered += OnEntered;
        _signaling.Outed += OnOuted;
    }

    private void OnDisable()
    {
        _signaling.Entered -= OnEntered;
        _signaling.Outed -= OnOuted;
    }

    private void OnEntered()
    {
        _violators++;
        SetVolume();
    }

    private void OnOuted()
    {
        _violators--;
        SetVolume();
    }

    private void SetVolume()
    {
        if (_violators == 0)
        {
            StartCoroutine(_signalizeTargetOff,_rateOfChange);
        }
        else
        {
            float rate = _rateOfChange * _violators;

            StartCoroutine(_signalizeTargetOn,rate);
        }
    }

    private void StartCoroutine(float targetVolume,float rate)
    {
        _coroutine = StartCoroutine(ChangeVolume(targetVolume,rate));
    }

    private IEnumerator ChangeVolume(float targetVolume, float rate)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, rate * Time.deltaTime);
            yield return null;
        }
    }
}
