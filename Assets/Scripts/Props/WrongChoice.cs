using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WrongChoice : Item
{
    [SerializeField] private BonusDoor _bonusDoor;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _bonusDoor.PutAwayBonus();

            progress.RemoveProgress(Value);

            if (other.TryGetComponent(out Player player))
                player.MadeWrongChoice();

            PlayEffect();
            DisableObject();
        }
    }
}