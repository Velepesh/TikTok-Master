using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovalProgressionDisplay : ProgressionDisplay
{
    readonly char sign = '-';

    private void OnEnable()
    {
        Wallet.RemovedProgression += OnRemovedProgression;
    }

    private void OnDisable()
    {
        Wallet.RemovedProgression -= OnRemovedProgression;
    }

    private void OnRemovedProgression(int respect)
    {
        ChangeValue(respect, sign);
    }
}