using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dancer : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.IncreaseRespect(Value);

            if (other.TryGetComponent(out Player player))
            {
                player.Rejoice();
            }

            PlayEffect();

            this.enabled = false;
        }
    }
}