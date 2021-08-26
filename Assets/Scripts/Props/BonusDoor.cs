using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDoor : Door
{
    [SerializeField] private Animator _animator;
    public void PutAwayBonus()
    {
        _animator.SetTrigger(AnimatorBonusDoorController.States.PutAway);
    }
}