using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class SkinView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _selectedButton;
    [SerializeField] private Sprite _lockIcon;

    private Customize _customise;
    private Image _currentBackground;

    public event UnityAction<Customize, SkinView> SelectedButtonClick;

    private void OnEnable()
    {
        _selectedButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _selectedButton.onClick.RemoveListener(OnButtonClick);
    }

    public void SelecteBackground(Sprite background)
    {
        _currentBackground = GetComponent<Image>();

        _currentBackground.sprite = background;
    }

    public void UnlockeView(Customize customize)
    {
        _selectedButton.interactable = true;
        _icon.sprite = customize.Icon;
    }

    public void Render(Customize customize)
    {
        _customise = customize;

        if (customize.IsBuyed)
            _icon.sprite = customize.Icon;
        else
            _icon.sprite = _lockIcon;

        TryLockItem();
    }

    private void TryLockItem()
    {
        if (_customise.IsBuyed == false)
            _selectedButton.interactable = false;
    }

    private void OnButtonClick()
    {
        if (_customise.IsBuyed)
        {
            SelectedButtonClick?.Invoke(_customise, this);
        }
    }
}