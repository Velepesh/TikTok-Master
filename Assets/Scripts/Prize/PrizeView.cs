using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PrizeView : MonoBehaviour
{
    [SerializeField] private Image _chestImage;
    [SerializeField] private GameObject _unlockItem;
    [SerializeField] private Button _selectedButton;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _awardText;

    private Prize _prize;

    public event UnityAction<Prize, PrizeView> PrizeButtonClick;

    private void OnEnable()
    {
        _selectedButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _selectedButton.onClick.RemoveListener(OnButtonClick);
    }

    public void UnlockeView()
    {
        _selectedButton.interactable = false;

        _chestImage.gameObject.SetActive(false);
        _unlockItem.SetActive(true);

        _animator.SetTrigger(AnimatorPrizeViewController.States.Unlock);
    }

    public void Render(Prize prize)
    {
        _prize = prize;
        _awardText.text = prize.Award.ToString();

        _chestImage.gameObject.SetActive(true);
    }

    private void OnButtonClick()
    {
        if (_keyCounter.KeysNumber > 0)
        {
            _keyCounter.DecreaseCounter();

            PrizeButtonClick?.Invoke(_prize, this);
            UnlockeView();
        }
    }
}
