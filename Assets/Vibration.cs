using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Vibration : MonoBehaviour
{
    [SerializeField] private HapticTypes _hapticType;
    [SerializeField] private bool _isUIScreen = false;

    readonly private string VibrationData = "VibrationData";
    public int IsOnInt => PlayerPrefs.GetInt(VibrationData, 1);
    public void Vibrate()
    {
        if (IsOnInt == 1)
        {
            if (_isUIScreen)
                MMVibrationManager.ContinuousHaptic(0.4f, 1f, 0.35f, _hapticType, this, true);
            else
                MMVibrationManager.Haptic(_hapticType);
        }
    }
}
