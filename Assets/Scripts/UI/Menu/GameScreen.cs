using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameScreen : Screen
{
    public event UnityAction HomeButtonClick;

    public override void Close()
    {
        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        ScreenHolder.SetActive(true);
    }

    protected override void OnButtonClick()
    {
        HomeButtonClick?.Invoke();
    }
}