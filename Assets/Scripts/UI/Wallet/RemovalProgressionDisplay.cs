using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovalProgressionDisplay : ProgressionDisplay
{
    readonly char sign = '-';

    private void OnEnable()
    {
        Progress.RemovedProgression += OnRemovedProgression;
    }

    private void OnDisable()
    {
        Progress.RemovedProgression -= OnRemovedProgression;
    }

    private void OnRemovedProgression(int respect)
    {
        ChangeValue(respect, sign);
    }
}