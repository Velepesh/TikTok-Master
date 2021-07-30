using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

class SkinChanger : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;

    private Skin _currentSkin;

    public event UnityAction SkinChanged;

    private void Awake()
    {
        TryWear();
    }

    public void ChangeSkin(SkinType newSkinType)
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

        _currentSkin = GetSkin(SkinType.Green);
    }
}