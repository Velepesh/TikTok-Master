using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private int _respects;

    private Animator _animator;

    private void OnValidate()
    {
        _respects = Mathf.Clamp(_respects, 0, 100);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            if (wallet.Respect >= _respects)
                _animator.SetTrigger(AnimatorGatesController.States.Open);
            else if (other.TryGetComponent(out Player player))
                player.Win();
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