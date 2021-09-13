using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelScreen : Screen
{
    [SerializeField] private StartScreen _startScreen;

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
}
