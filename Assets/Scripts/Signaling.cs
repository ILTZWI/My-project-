using System;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    public event Action Entered;
    public event Action Outed;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Entered.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Outed.Invoke();
        }
    } 
}