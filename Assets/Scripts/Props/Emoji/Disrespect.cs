using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disrespect : Item
{
    [SerializeField] private Vibration _vibration;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _vibration.Vibrate();
            DisableObject();
           
            progress.RemoveProgress(Value);

            PlayEffect();
        }
    }
}