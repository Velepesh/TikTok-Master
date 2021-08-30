using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private int _progressToSkip;
    [SerializeField] private int _multiplier;

    private Animator _animator;

    private void OnValidate()
    {
        _progressToSkip = Mathf.Clamp(_progressToSkip, 0, 100);
        _multiplier = Mathf.Clamp(_multiplier, 2, 5);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            if (wallet.Progression >= _progressToSkip)
            {
                wallet.ApplyMultiplier(_multiplier);

                _animator.SetTrigger(AnimatorGatesController.States.Open);
            }
            else if (other.TryGetComponent(out Player player))
            {
                player.Win();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            this.gameObject.SetActive(false);
        }
    }
}