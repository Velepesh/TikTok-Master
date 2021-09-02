using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(Wallet), typeof(SkinChangerStage))]
class SkinChanger : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryContainer;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private SkinType _startSkin;

    private List<SkinHolder> _skinHolders = new List<SkinHolder>();
    private Skin _currentSkin;
    private SkinChangerStage _stage;
    private Wallet _respect;
    private float _previousValue;

    public event UnityAction SkinChanged;

    private void Awake()
    {
        _respect = GetComponent<Wallet>();
        _stage = GetComponent<SkinChangerStage>();

        GetPlayerSkins();
        TryWear();
    }

    private void OnEnable()
    {
        _respect.ProgressionChanged += OnProgressionChanged;
    }

    private void Start()
    {
        _previousValue = _respect.Progression;
    }

    private void OnDisable()
    {
        _respect.ProgressionChanged -= OnProgressionChanged;
    }

    private void GetPlayerSkins()
    {
        for (int i = 0; i < _inventory.GetCountOfHolders(); i++)
        {
            Customize customize = _inventory.GetSkinsHolder(i);

            if (customize.IsBuyed)
            {
                Instantiate(customize.Skin, _inventoryContainer.transform).TryGetComponent(out SkinHolder skinHolder);
                Debug.Log(skinHolder.gameObject.name);
                _skinHolders.Add(skinHolder);
                skinHolder.gameObject.SetActive(false);
            }
        }
    }

    public void NextWeapon()
    {
        //if (_currentWeaponNumber == _weapons.Count - 1)
        //    _currentWeaponNumber = 0;
        //else
        //    _currentWeaponNumber++;

        //_currentWeapon.gameObject.SetActive(false);
        //ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        //if (_currentWeaponNumber == 0)
        //    _currentWeaponNumber = _weapons.Count - 1;
        //else
        //    _currentWeaponNumber--;

        //_currentWeapon.gameObject.SetActive(false);
        //ChangeWeapon(_weapons[_currentWeaponNumber]);
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
        var newSkin = GetSkin(newSkinType);
        _currentSkin.gameObject.SetActive(false);

        _currentSkin = newSkin;
        _currentSkin.gameObject.SetActive(true);

        SkinChanged?.Invoke();
    }

    private Skin GetSkin(SkinType type)
    {
        var variants = _skins.Where(skin => skin.SkinType == type);

        if (variants.Count() == 0)
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