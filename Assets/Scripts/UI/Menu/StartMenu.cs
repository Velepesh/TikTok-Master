using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Button _passiveIncomeButton;
    [SerializeField] private Button _increaseRespectButton;
    [SerializeField] private TMP_Text _priceText;

    readonly private int _price = 500;
    readonly private int _increaseRespect = 10000;

    private void Awake()
    {
        _priceText.text = _price.ToString();
    }

    private void OnEnable()
    {
        _passiveIncomeButton.onClick.AddListener(OnPassiveIncomeButton);
        _increaseRespectButton.onClick.AddListener(OnIncreaseRespectButton);
    }

    private void OnDisable()
    {
        _passiveIncomeButton.onClick.RemoveListener(OnPassiveIncomeButton);
        _increaseRespectButton.onClick.RemoveListener(OnIncreaseRespectButton);
    }

    private void OnPassiveIncomeButton()
    {

    }

    private void OnIncreaseRespectButton()
    {
        if(_wallet.Subscriber >= _price)
        {
            _wallet.RemoveSubscriber(_price);
            _wallet.AddRespect(_increaseRespect);
        }
    }
}
