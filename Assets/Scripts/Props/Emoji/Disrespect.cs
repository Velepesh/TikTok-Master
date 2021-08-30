using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class Disrespect : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.RemoveRespect(Value);

            PlayEffect();
            DisableObject();
        }
    }
}