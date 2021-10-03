using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Vibration : MonoBehaviour
{
    [SerializeField] private VibrationSetting _setting;
    [SerializeField] private Player _player;
    [SerializeField] private Progress _progress;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private int _lightImpactValue = 10;
    [SerializeField] private int _mediumImpactValue = 20;

    private void OnValidate()
    {
        _lightImpactValue = Mathf.Clamp(_lightImpactValue, 0, 100);
        _mediumImpactValue = Mathf.Clamp(_mediumImpactValue, 0, 100);
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.GameOver += OnGameOver;
        _progress.AddedProgression += OnAddedProgression;
        _progress.RemovedProgression += OnRemovedProgression;
        _keyCounter.KeysNumberChanged += OnKeysNumberChanged;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.GameOver -= OnGameOver;
        _progress.AddedProgression -= OnAddedProgression;
        _progress.RemovedProgression -= OnRemovedProgression;
        _keyCounter.KeysNumberChanged -= OnKeysNumberChanged;
    }

    private void OnWon()
    {
        VibrateContinuous();
    }

    private void OnGameOver()
    {
        VibrateContinuous();
    }

    private void OnAddedProgression(int value)
    {
        VibrateShort(SelectHapticType(value));
    }

    private void OnRemovedProgression(int value)
    {
        VibrateShort(SelectHapticType(value));
    }

    private HapticTypes SelectHapticType(int value)
    {
        if (value <= _lightImpactValue)
            return HapticTypes.LightImpact;
        else if (value <= _mediumImpactValue)
            return HapticTypes.MediumImpact;
        else
            return HapticTypes.HeavyImpact;
    }

    private void OnKeysNumberChanged(int value)
    {
        VibrateShort(HapticTypes.HeavyImpact);
    }

    private void VibrateShort(HapticTypes type)
    {
        if (_setting.IsOn)
            MMVibrationManager.Haptic(type);
    }

    private void VibrateContinuous()
    {
        if (_setting.IsOn)
            MMVibrationManager.ContinuousHaptic(0.4f, 1f, 0.4f, HapticTypes.None, this, true);
    }
}
