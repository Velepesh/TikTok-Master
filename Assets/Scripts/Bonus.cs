using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bonus : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price = 500;
    [SerializeField] private int _award = 10000;

    private void Awake()
    {
        _priceText.text = _price.ToString();
    }

    public void IncreaseBonus()
    {
        if (_wallet.Subscriber >= _price)
        {
            _wallet.RemoveSubscriber(_price);
            _wallet.AddRespect(_award);
        }
    }
}