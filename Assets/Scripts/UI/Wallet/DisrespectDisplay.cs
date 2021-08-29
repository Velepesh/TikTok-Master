using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisrespectDisplay : OnLevelCollisionDisplay
{
    readonly char sign = '-';

    private void OnEnable()
    {
        Wallet.DisrespectMatched += OnDisrespectMatched;
    }

    private void OnDisable()
    {
        Wallet.DisrespectMatched -= OnDisrespectMatched;
    }

    private void OnDisrespectMatched(int respect)
    {
        ChangeValue(respect, sign);
    }
}