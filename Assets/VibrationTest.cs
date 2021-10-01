using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationTest : MonoBehaviour
{
    public HapticTypes HapticTypes = HapticTypes.Heavy;
    public void VibrateTest()
    {
        MMVibrationManager.ContinuousHaptic(0.3f, 0.8f, 3.5f, HapticTypes.None, this, true);
    }
}
