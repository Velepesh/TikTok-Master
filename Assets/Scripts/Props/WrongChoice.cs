using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WrongChoice : Item
{
    [SerializeField] private BonusDoor _bonusDoor;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            _bonusDoor.PutAwayBonus();

            wallet.RemoveRespect(Value);

            if (other.TryGetComponent(out Player player))
                player.MadeWrongChoice();

            PlayEffect();
            DisableObject();
        }
    }
}