using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelScreen : Screen
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private FortuneWheelGame _fortuneWheelGame;

    private void Start()
    {
        _fortuneWheelGame.SpinStarted += OnSpinStarted;
        _fortuneWheelGame.SpinEnded += OnSpinEnded;
    }

    private void OnDisable()
    {
        _fortuneWheelGame.SpinStarted -= OnSpinStarted;
        _fortuneWheelGame.SpinEnded -= OnSpinEnded;
    }

    public override void Close()
    {
        _startScreen.Open();

        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        _startScreen.Close();

        ScreenHolder.SetActive(true);
    }

    protected override void OnButtonClick()
    {
        Close();
    }

    private void OnSpinStarted()
    {
        Button.interactable = false;
    }

    private void OnSpinEnded()
    {
        Button.interactable = true;
    }
}
