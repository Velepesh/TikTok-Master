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
            if (currentValue >= _stage.FifthValue && _previousValue < _stage.FifthValue)
            {
                ChangeSkin(SkinType.Fifth);
            }

            if (currentValue >= _stage.FourthValue && _previousValue < _stage.FourthValue)
            {
                ChangeSkin(SkinType.Fourth);
            }

            if (currentValue >= _stage.ThirdValue && _previousValue < _stage.ThirdValue)
            {
                ChangeSkin(SkinType.Third);
            }

            if (currentValue >= _stage.SecondValue && _previousValue < _stage.SecondValue)
            {
                ChangeSkin(SkinType.Second);
            }

            if (currentValue >= _stage.FirstStage && _previousValue < _stage.FirstStage)
            {
                ChangeSkin(SkinType.First);
            }
        }
        else 
        { 
            if (currentValue < _stage.FifthValue && _previousValue >= _stage.FourthValue)
            {
                ChangeSkin(SkinType.Fourth);
            }

            if (currentValue < _stage.FourthValue && _previousValue >= _stage.ThirdValue)
            {
                ChangeSkin(SkinType.Third);
            }

            if (currentValue < _stage.ThirdValue && _previousValue >= _stage.SecondValue)
            {
                ChangeSkin(SkinType.Second);
            }

            if (currentValue < _stage.SecondValue && _previousValue >= _stage.FirstStage)
            {
                ChangeSkin(SkinType.First);
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