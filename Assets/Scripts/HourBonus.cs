using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HourBonus : MonoBehaviour
{
    [SerializeField] private int _award;
    [SerializeField] private int _price;
    [SerializeField] private int _additionAward;
    [SerializeField] private int _additionPrice;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _t;

    readonly private string AwardData = "AwardtData";
    readonly private string PriceData = "PriceData";

    public int Award => PlayerPrefs.GetInt(AwardData, 0);
    public int Price => PlayerPrefs.GetInt(PriceData, 0);

    public event UnityAction<int> BonusIncreased;

    private void OnValidate()
    {
        _award = Mathf.Clamp(_award, 0, int.MaxValue);
        _price = Mathf.Clamp(_price, 0, int.MaxValue);
    }

    private void Start()
    {
        BonusIncreased?.Invoke(_price);
    }

    public void IncreaseBonus()
    {
        if (_wallet.Subscriber >= _price)
        {
            _wallet.RemoveSubscriber(_price);
            // _wallet.AddRespect(_award);
            AddAward();
            AddPrice();

            BonusIncreased?.Invoke(_price);
        }
    }

    private void AddAward()
    {
        _award += _additionAward;

        SaveAwardData();
    }

    private void AddPrice()
    {
        _price += _additionPrice;

        SavePriceData();
    }

    private void SaveAwardData()
    {
        PlayerPrefs.SetInt(AwardData, _award);
    }

    private void SavePriceData()
    {
        PlayerPrefs.SetInt(PriceData, _price);
    }
}