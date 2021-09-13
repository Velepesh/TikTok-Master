using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Button _fortuneWheelButton;
    [SerializeField] private Button _passiveIncomeButton;
    [SerializeField] private Button _increaseRespectButton;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _incomePrice = 500;
    [SerializeField] private FortuneWheelScreen _fortuneScreen;

    readonly private int _increaseRespect = 10000;

    private void Awake()
    {
        _priceText.text = _incomePrice.ToString();
    }

    private void OnEnable()
    {
        _fortuneWheelButton.onClick.AddListener(OnFortuneWheelButton);
        _passiveIncomeButton.onClick.AddListener(OnPassiveIncomeButton);
        _increaseRespectButton.onClick.AddListener(OnIncreaseRespectButton);
    }

    private void OnDisable()
    {
        _fortuneWheelButton.onClick.RemoveListener(OnFortuneWheelButton);
        _passiveIncomeButton.onClick.RemoveListener(OnPassiveIncomeButton);
        _increaseRespectButton.onClick.RemoveListener(OnIncreaseRespectButton);
    }

    private void OnFortuneWheelButton()
    {
        _fortuneScreen.Open();
    }

    private void OnPassiveIncomeButton()
    {

    }

    private void OnIncreaseRespectButton()
    {
        if(_wallet.Subscriber >= _incomePrice)
        {
            _wallet.RemoveSubscriber(_incomePrice);
            _wallet.AddRespect(_increaseRespect);
        }
    }
}
