using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    public event UnityAction PlayButtonClick;

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
        PlayButtonClick?.Invoke();
    }
}