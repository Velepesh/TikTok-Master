using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Income : MonoBehaviour
{
    [SerializeField] private int _award;
    [SerializeField] private int _price;
    [SerializeField] private int _additionAward;
    [SerializeField] private int _additionPrice;
    [SerializeField] private Wallet _wallet;

    readonly private string _awardData = "AwardtData";
    readonly private string _priceData = "PriceData";

    public int Award => PlayerPrefs.GetInt(_awardData, _award);
    public int Price => PlayerPrefs.GetInt(_priceData, _price);

    public event UnityAction<int> BonusIncreased;

    private void OnValidate()
    {
        _award = Mathf.Clamp(_award, 0, int.MaxValue);
        _price = Mathf.Clamp(_price, 0, int.MaxValue);
    }

    private void Start()
    {
        _award = Award;
        _price = Price;

        BonusIncreased?.Invoke(_price);
    }

    public void EarnIncome()
    {
        if (_wallet.Subscriber >= _price)
        {
            _wallet.RemoveSubscriber(_price);
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
        PlayerPrefs.SetInt(_awardData, _award);
    }

    private void SavePriceData()
    {
        PlayerPrefs.SetInt(_priceData, _price);
    }
}