using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncomeDisplay : MonoBehaviour
{
    [SerializeField] private Income _hourBonus;
    [SerializeField] private TMP_Text _priceText;

    private void OnEnable()
    {
        _hourBonus.BonusIncreased += OnBonusIncreased;
    }

    private void OnDisable()
    {
        _hourBonus.BonusIncreased -= OnBonusIncreased;
    }

    private void OnBonusIncreased(int price)
    {
        _priceText.text = price.ToString();
    }
}