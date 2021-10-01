using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseScreen : Screen
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _continueButton;

    public event UnityAction ExitButtonClick;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _continueButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _continueButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public override void Close()
    {
        ScreenHolder.SetActive(false);

        Time.timeScale = 1;
    }

    public override void Open()
    {
        ScreenHolder.SetActive(true);

        Time.timeScale = 0;
    }

    protected override void OnButtonClick()
    {
        Time.timeScale = 1;
        ExitButtonClick?.Invoke();
    }

    private void OnCloseButtonClick()
    {
        Close();
    }
}