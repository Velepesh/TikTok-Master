using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeScreen : Screen
{
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
        throw new System.NotImplementedException();
    }
}
