using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private Vibration _vibration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _keyCounter.IncreaseCounter();
            _vibration.Vibrate();

            gameObject.SetActive(false);
        }
    }
}