using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Wallet), typeof(SkinChangerStage))]
class SkinChanger : MonoBehaviour
{
    [SerializeField] private SkinType _startSkinType;
    [SerializeField] private List<SkinsHolder> _holders;
    [SerializeField] private Shop _shop;
    [SerializeField] private float _inShopTime;
   
    private Skin _currentSkin;
    private SkinChangerStage _stage;
    private Progress _progress;
    private float _previousValue;
    private List<Skin> _skins = new List<Skin>();
    private SkinsHolder _currentHolder;
    private bool _isShopOpen;
    private float _elapsedTime;
    private int _shopValue;

    public SkinType StartSkinType => _startSkinType;

    public event UnityAction SkinChanged;
    public event UnityAction<SkinType> StageChanged;

    private void Awake()
    {
        _progress = GetComponent<Progress>();
        _stage = GetComponent<SkinChangerStage>();
    }

    private void Update()
    {
        if (_isShopOpen)
        {
            if(_elapsedTime <= 0f)
            {
                UpdateSkin();
                _elapsedTime = _inShopTime;
            }

            _elapsedTime -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        _progress.ProgressionChanged += OnProgressionChanged;
        _shop.SelectedHolder += OnSelectedHolder;
        _shop.ChoosedSkin += OnChoosedSkin;
        _shop.Closed += OnClosed;
    }

    private void OnDisable()
    {
        _progress.ProgressionChanged -= OnProgressionChanged;
        _shop.SelectedHolder -= OnSelectedHolder;
        _shop.ChoosedSkin -= OnChoosedSkin;
        _shop.Closed -= OnClosed;
    }

    private void Start()
    {
        _previousValue = _progress.Progression;

        _currentHolder = _shop.GetCurrentHolder();

        _currentSkin = GetStartSkin();
    }

    private void UpdateSkin()
    {
        if (_shopValue == _stage.NerdValue)
        {
            ChangeSkin(SkinType.Nerd);
            _shopValue = _stage.ClerkValue;
        }
        else if (_shopValue == _stage.ClerkValue)
        {
            ChangeSkin(SkinType.Clerk);
            _shopValue = _stage.OrdinaryValue;
        }
        else if (_shopValue == _stage.OrdinaryValue)
        {
            ChangeSkin(SkinType.Ordinary);
            _shopValue = _stage.StylishValue;
        }
        else if (_shopValue == _stage.StylishValue)
        {
            ChangeSkin(SkinType.Stylish);
            _shopValue = _stage.TiktokerValue;
        }
        else if (_shopValue == _stage.TiktokerValue)
        {
            ChangeSkin(SkinType.Tiktoker);
            _shopValue = _stage.NerdValue;
        }

        SkinChanged?.Invoke();
    }

    private void OnChoosedSkin()
    {
        _elapsedTime = 0f;
        _isShopOpen = true;
        _shopValue = _stage.NerdValue;
    }

    private void OnClosed()
    {
        _isShopOpen = false;

        ChangeSkin(_startSkinType);
    }

    private Skin GetStartSkin()
    {
        return _currentHolder.GetSkin(_startSkinType);
    }
    
    private void OnSelectedHolder(SkinsHolder skinsHolder)
    {
        _currentHolder = skinsHolder;

        _skins = _currentHolder.GetSkins();

        TryWear();
    }

    private void OnProgressionChanged(int currentTikTokValue, int maxValue)
    {
        int vector = 0;

        if (currentTikTokValue >= _previousValue)
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
                ChangeSkin(SkinType.Tiktoker);
            else if (currentValue >= _stage.StylishValue && _previousValue < _stage.StylishValue)
                ChangeSkin(SkinType.Stylish);
            else if (currentValue >= _stage.OrdinaryValue && _previousValue < _stage.OrdinaryValue)
                ChangeSkin(SkinType.Ordinary);
            else if (currentValue >= _stage.ClerkValue && _previousValue < _stage.ClerkValue)
                ChangeSkin(SkinType.Clerk);
            else if (currentValue >= _stage.NerdValue && _previousValue < _stage.NerdValue)
                ChangeSkin(SkinType.Nerd);
        }
        else
        {
            if (currentValue < _stage.TiktokerValue && _previousValue >= _stage.StylishValue)
                ChangeSkin(SkinType.Stylish);

            if (currentValue < _stage.StylishValue && _previousValue >= _stage.OrdinaryValue)
                ChangeSkin(SkinType.Ordinary);

            if (currentValue < _stage.OrdinaryValue && _previousValue >= _stage.ClerkValue)
                ChangeSkin(SkinType.Clerk);

            if (currentValue < _stage.ClerkValue && _previousValue >= _stage.NerdValue)
                ChangeSkin(SkinType.Nerd);
        }
    }

    private void ChangeSkin(SkinType newSkinType)
    {
        var newSkin = _currentHolder.GetSkin(newSkinType);
        _currentSkin.gameObject.SetActive(false);

        _currentSkin = newSkin;
        _currentSkin.gameObject.SetActive(true);

        SkinChanged?.Invoke();
        StageChanged?.Invoke(newSkinType);
    }

    private void TryWear()
    {
        if (_skins.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(_skins));
       
        _currentSkin = _currentHolder.GetSkin(_startSkinType);
    }
}