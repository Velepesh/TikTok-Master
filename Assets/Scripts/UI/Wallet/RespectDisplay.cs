using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespectDisplay : OnLevelCollisionDisplay
{
    readonly char sign = '+';

    private void OnEnable()
    {
        Wallet.RespectMatched += OnRespectMatched;
    }

    private void OnDisable()
    {
        Wallet.RespectMatched -= OnRespectMatched;
    }

    private void OnRespectMatched(int respect)
    {
        ChangeValue(respect, sign);
    }
}