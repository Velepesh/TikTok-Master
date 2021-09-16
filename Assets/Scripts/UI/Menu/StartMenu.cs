using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _fortuneWheelButton;
    [SerializeField] private Button _hourBonusButton;
    [SerializeField] private Button _increaseRespectButton;
    [SerializeField] private FortuneWheelScreen _fortuneScreen;
    [SerializeField] private Income _income;
    [SerializeField] private HourBonus _hourBonus;

    private void OnEnable()
    {
        _fortuneWheelButton.onClick.AddListener(OnFortuneWheelButton);
        _hourBonusButton.onClick.AddListener(OnHourBonusButton);
        _increaseRespectButton.onClick.AddListener(OnIncomeButton);
    }

    private void OnDisable()
    {
        _fortuneWheelButton.onClick.RemoveListener(OnFortuneWheelButton);
        _hourBonusButton.onClick.RemoveListener(OnHourBonusButton);
        _increaseRespectButton.onClick.RemoveListener(OnIncomeButton);
    }

    private void OnFortuneWheelButton()
    {
        _fortuneScreen.Open();
    }

    private void OnHourBonusButton()
    {
        _hourBonus.IncreaseBonus();
    }

    private void OnIncomeButton()
    {
        _income.EarnIncome();
    }
}