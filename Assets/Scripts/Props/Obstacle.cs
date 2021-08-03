using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Obstacle : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.DecreaseRespect(Value);

            if (wallet.Respect > 0)
                if (other.TryGetComponent(out Player player))
                    player.Stumble();

            PlayEffect();
        }
    }
}