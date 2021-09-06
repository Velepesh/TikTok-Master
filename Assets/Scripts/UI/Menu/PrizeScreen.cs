using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrizeScreen : Screen
{
    public event UnityAction NextButtonClick;
    
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
        NextButtonClick?.Invoke();
    }
}