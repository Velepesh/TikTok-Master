using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinChanger))]
public class SkinChangerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _changeEffect;

    private SkinChanger _skinChanger;

    private void Awake()
    {
        _skinChanger = GetComponent<SkinChanger>();
    }

    private void OnEnable()
    {
        _skinChanger.SkinChanged += OnSkinChanged;
    }

    private void OnDisable()
    {
        _skinChanger.SkinChanged -= OnSkinChanged;
    }

    private void OnSkinChanged()
    {
        _changeEffect.Play();
    }
}
