using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RightChoice : Item
{
    [SerializeField] private BonusDoor _bonusDoor;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.AddRespect(Value);

            if (other.TryGetComponent(out Player player))
                player.MadeRightChoice();

            PlayEffect();
            DisableObject();
        }
    }
}