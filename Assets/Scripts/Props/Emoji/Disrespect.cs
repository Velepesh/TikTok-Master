using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disrespect : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            DisableObject();
           
            progress.RemoveProgress(Value);

            PlayAudio();
            PlayEffect();
        }
    }
}