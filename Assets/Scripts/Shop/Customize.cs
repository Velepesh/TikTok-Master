using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Customize", menuName = "Customize/Skin", order = 51)]
public class Customize : ScriptableObject
{
    [SerializeField] private GameObject _skinsHolder;
    [SerializeField] private Sprite _icon;
    [SerializeField] private HolderType _type;
    [SerializeField] private bool _isBuyed;
    [SerializeField] private string _label;

    private string _customizeData => _label;

    public GameObject SkinsHolder => _skinsHolder;
    public Sprite Icon => _icon;
    public HolderType Type => _type;
    public bool IsBuyed => _isBuyed;

    private void OnEnable()
    {
        _isBuyed = Convert.ToBoolean(PlayerPrefs.GetInt(_customizeData, 0));
    }

    public void Buy()
    {
        _isBuyed = true;

        PlayerPrefs.SetInt(_customizeData, 1);
        SaveCustomizeData(ÑonvertBoolToInt(_isBuyed));
    }

    private int ÑonvertBoolToInt(bool value)
    {
        if (value == true)
            return 1;
        else
            return 0;
    }

    private void SaveCustomizeData(int value)
    {
        PlayerPrefs.SetInt(_customizeData, value);
    }
}