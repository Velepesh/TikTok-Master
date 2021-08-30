using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddingProgressionDisplay : ProgressionDisplay
{
    readonly char sign = '+';

    private void OnEnable()
    {
        Wallet.AddedProgression += OnAddedProgression;
    }

    private void OnDisable()
    {
        Wallet.AddedProgression -= OnAddedProgression;
    }

    private void OnAddedProgression(int respect)
    {
        ChangeValue(respect, sign);
    }
}