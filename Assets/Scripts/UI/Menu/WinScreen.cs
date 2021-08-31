using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class WinScreen : Screen
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _delayTime;

    public event UnityAction NextButtonClick;

    public override void Close()
    {
        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        StartCoroutine(EnableWinScreen());
    }

    protected override void OnButtonClick()
    {
        NextButtonClick?.Invoke();
    }

    private IEnumerator EnableWinScreen()
    {
        yield return new WaitForSeconds(_delayTime);

        _scoreText.text = _wallet.LevelRespect.ToString();
        ScreenHolder.SetActive(true);
    }
}