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
    private Wallet _respect;
    private float _previousValue;
    private List<Skin> _skins = new List<Skin>();
    private SkinsHolder _currentHolder;
    private bool _isShopOpen;
    private float _elapsedTime;
    private int _shopValue;

    public event UnityAction SkinChanged;

    private void Awake()
    {
        _respect = GetComponent<Wallet>();
        _stage = GetComponent<SkinChangerStage>();
    }

    private void Update()
    {
        if (_isShopOpen)
        {
            if(_elapsedTime <= 0f)
            {
                s();
                _elapsedTime = _inShopTime;
            }

            _elapsedTime -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        _respect.ProgressionChanged += OnProgressionChanged;
        _shop.SelectedHolder += OnSelectedHolder;
        _shop.ChoosedSkin += OnChoosedSkin;
        _shop.Closed += OnClosed;
    }

    private void OnDisable()
    {
        _respect.ProgressionChanged -= OnProgressionChanged;
        _shop.SelectedHolder -= OnSelectedHolder;
        _shop.ChoosedSkin -= OnChoosedSkin;
        _shop.Closed -= OnClosed;
    }

    private void Start()
    {
        _previousValue = _respect.Progression;

        _currentHolder = _shop.GetCurrentHolder();

        _currentSkin = GetStartSkin();
    }

    private void s()
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
    private IEnumerator UpdateSkin()
    {
        int currentValue = _stage.NerdValue;

        while (true)
        {
            if (currentValue == _stage.NerdValue)
            {
                ChangeSkin(SkinType.Nerd);
                currentValue = _stage.ClerkValue;
            }
            else if(currentValue == _stage.ClerkValue)
            {
                ChangeSkin(SkinType.Clerk);
                currentValue = _stage.OrdinaryValue;
            }
            else if (currentValue == _stage.OrdinaryValue)
            {
                ChangeSkin(SkinType.Ordinary);
                currentValue = _stage.StylishValue;
            }
            else if (currentValue == _stage.StylishValue)
            {
                ChangeSkin(SkinType.Stylish);
                currentValue = _stage.TiktokerValue;
            }
            else if (currentValue == _stage.TiktokerValue)
            {
                ChangeSkin(SkinType.Tiktoker);
                currentValue = _stage.NerdValue;
            }

            SkinChanged?.Invoke();

            yield return new WaitForSeconds(_inShopTime);
        }
    }

    private void OnChoosedSkin()
    {
        // StopCoroutine(UpdateSkin());
        _elapsedTime = 0f;
        _isShopOpen = true;
        _shopValue = _stage.NerdValue;
        //StartCoroutine(UpdateSkin());
    }

    private void OnClosed()
    {
        _isShopOpen = false;

        //StopCoroutine(UpdateSkin());
        ChangeSkin(_startSkinType);
       // SkinChanged?.Invoke();
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
    }

    private void TryWear()
    {
        if (_skins.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(_skins));
       
        _currentSkin = _currentHolder.GetSkin(_startSkinType);
    }

    private void SaveSkinChangereData(int index)
    {
       // PlayerPrefs.SetInt(SkinChangereData, index);
    }
}