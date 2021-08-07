using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof (Wallet), typeof(SkinChangerStage))]
class SkinChanger : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private SkinType _startSkin;

    private Skin _currentSkin;
    private SkinChangerStage _stage;
    private Wallet _wallet;
    private float _previousValue;

    public event UnityAction SkinChanged;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _stage = GetComponent<SkinChangerStage>();

        TryWear();
    }

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
    }

    private void OnRespectChanged(int currentTikTokValue, int maxValue)
    {
        int vector = 0;

        if (currentTikTokValue > _previousValue)
            vector = 1;
        else
            vector = -1;

        ChangePlayerSkin(currentTikTokValue, vector);

        _previousValue = currentTikTokValue;
    }

    private void ChangePlayerSkin(int currentValue, int vector)
    {
        if (vector > 0)
        {
            if (currentValue >= _stage.TiktokerValue && _previousValue < _stage.TiktokerValue)
            {
                ChangeSkin(SkinType.Tiktoker);
            }

            if (currentValue >= _stage.StylishValue && _previousValue < _stage.StylishValue)
            {
                ChangeSkin(SkinType.Stylish);
            }

            if (currentValue >= _stage.OrdinaryValue && _previousValue < _stage.OrdinaryValue)
            {
                ChangeSkin(SkinType.Ordinary);
            }

            if (currentValue >= _stage.ClerkValue && _previousValue < _stage.ClerkValue)
            {
                ChangeSkin(SkinType.Clerk);
            }

            if (currentValue >= _stage.NerdValue && _previousValue < _stage.NerdValue)
            {
                ChangeSkin(SkinType.Nerd);
            }
        }
        else 
        { 
            if (currentValue < _stage.TiktokerValue && _previousValue >= _stage.StylishValue)
            {
                ChangeSkin(SkinType.Stylish);
            }

            if (currentValue < _stage.StylishValue && _previousValue >= _stage.OrdinaryValue)
            {
                ChangeSkin(SkinType.Ordinary);
            }

            if (currentValue < _stage.OrdinaryValue && _previousValue >= _stage.ClerkValue)
            {
                ChangeSkin(SkinType.Clerk);
            }

            if (currentValue < _stage.ClerkValue && _previousValue >= _stage.NerdValue)
            {
                ChangeSkin(SkinType.Nerd);
            }

        }
    }
    private void ChangeSkin(SkinType newSkinType)
    {
        var newSkin = GetSkin(newSkinType);
        _currentSkin.gameObject.SetActive(false);

        _currentSkin = newSkin;
        _currentSkin.gameObject.SetActive(true);

        SkinChanged?.Invoke();
    }

    private Skin GetSkin(SkinType type)
    {
        var variants = _skins.Where(skin => skin.SkinType == type);

        if(variants.Count() == 0)
            throw new ArgumentOutOfRangeException(nameof(type));

        return variants.First();
    }

    private void TryWear()
    {
        if (_skins.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(_skins));

        _currentSkin = GetSkin(_startSkin);
    }
}