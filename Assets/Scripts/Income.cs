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

    readonly private string AwardData = "AwardtData";
    readonly private string PriceData = "PriceData";

    public int Award => PlayerPrefs.GetInt(AwardData, _award);
    public int Price => PlayerPrefs.GetInt(PriceData, _price);

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
        PlayerPrefs.SetInt(AwardData, _award);
    }

    private void SavePriceData()
    {
        PlayerPrefs.SetInt(PriceData, _price);
    }
}