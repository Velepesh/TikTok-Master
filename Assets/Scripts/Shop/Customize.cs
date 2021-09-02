using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customize", menuName = "Customize/Skin", order = 51)]
public class Customize : ScriptableObject
{
    [SerializeField] private GameObject _skin;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed;

    readonly private string CustomizeData = "CustomizeData";

    private int IsBuyedInt => PlayerPrefs.GetInt(CustomizeData, 0);

    public GameObject Skin => _skin;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;


    public void Buy()
    {
        _isBuyed = true;

        PlayerPrefs.SetInt(CustomizeData, 1);
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
        PlayerPrefs.SetInt(CustomizeData, value);
    }
}