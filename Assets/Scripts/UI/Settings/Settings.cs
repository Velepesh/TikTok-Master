using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _button;

    private bool _isOpen = false;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);

        _isOpen = false;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (_isOpen)
            Close();
        else
            Open();
    }

    private void Open()
    {
        _isOpen = true;

        _animator.SetTrigger(AnimatorSettingsController.States.Open);
    }

    private void Close()
    {
        _isOpen = false;

        _animator.SetTrigger(AnimatorSettingsController.States.Close);
    }
}