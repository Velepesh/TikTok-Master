using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinScreen : Screen
{
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

        ScreenHolder.SetActive(true);
    }
}