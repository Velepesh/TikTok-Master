using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
    }

    private void OnRespectChanged(int respect, int maxRespect)
    {

    }
}