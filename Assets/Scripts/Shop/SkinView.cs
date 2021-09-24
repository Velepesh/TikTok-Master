using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _avatarIcon;
    [SerializeField] private Button _selectedButton;
    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private Sprite _unlockIcon;
    [SerializeField] private Sprite _selectedIcon;

    private Customize _customise;

    public event UnityAction<Customize, SkinView> SelectedButtonClick;

    private void OnEnable()
    {
        _selectedButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _selectedButton.onClick.RemoveListener(OnButtonClick);
    }

    private void Awake()
    {
        _background = GetComponent<Image>();
    }

    public void SelecteView()
    {
        _background.sprite = _selectedIcon;
    }
    
    public void DisableView()
    {
        _background.sprite = _unlockIcon;
    }

    public void UnlockeView(Customize customize)
    {
        _selectedButton.interactable = true;

        Unlock(customize);
    }

    public void Render(Customize customize)
    {
        _customise = customize;

        if (customize.IsBuyed)
            Unlock(customize);
        else
            TryLockItem();
    }

    private void Unlock(Customize customize)
    {
        _avatarIcon.gameObject.SetActive(true);
        _avatarIcon.sprite = customize.Icon;
        _background.sprite = _unlockIcon;
    }

    private void TryLockItem()
    {
        if (_customise.IsBuyed == false)
        {
            _selectedButton.interactable = false;

            _background.sprite = _lockIcon;
            _avatarIcon.gameObject.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        if (_customise.IsBuyed)
        {
            SelectedButtonClick?.Invoke(_customise, this);
        }
    }
}