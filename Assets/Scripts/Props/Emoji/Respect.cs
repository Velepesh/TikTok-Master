using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respect : Item
{
    [SerializeField] private Vibration _vibration;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            progress.AddProgress(Value);

            _vibration.Vibrate();
            PlayEffect();
            DisableObject();
        }
    }
}