using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopScreen : Screen
{
    [SerializeField] private Button _closeButton;

    public event UnityAction CustomizeButtonClick;
    public event UnityAction CloseButtonClick;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

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
        CustomizeButtonClick?.Invoke();
    }

    private void OnCloseButtonClick()
    {
        Close();

        CloseButtonClick?.Invoke();
    }
}