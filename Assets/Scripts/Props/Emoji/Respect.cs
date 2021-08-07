using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Respect : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.IncreaseRespect(Value);

            PlayEffect();
            DisableObject();
        }
    }
}