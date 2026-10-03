using UnityEditor.Media;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Signaling _signaling;
    [SerializeField] private float _rateOfChange;

    private float _signalizeTargetOn = 1;
    private float _signalizeTargetOff = 0;

    private int _violators = 0;

    private void Update()
    {
        ChangeVolume();
    }

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
    }

    private void OnOuted()
    {
        _violators--;
    }

    private void ChangeVolume()
    {
        if (_violators == 0)
        {
            DownVolume();
        }
        else
        {
            UpVolume();
        }
    }

    private void UpVolume()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _signalizeTargetOn, (_rateOfChange * _violators) * Time.deltaTime);
    }

    private void DownVolume()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _signalizeTargetOff, _rateOfChange * Time.deltaTime);
    }
}
