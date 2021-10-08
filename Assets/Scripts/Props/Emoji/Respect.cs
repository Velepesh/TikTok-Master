using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respect : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            progress.AddProgress(Value);

           // PlayAudio();
            PlayEffect();
            DisableObject();
        }
    }
}