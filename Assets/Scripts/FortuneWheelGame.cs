using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class FortuneWheelGame : MonoBehaviour
{
    [SerializeField] private PickerWheel _pickerWheel;
    [SerializeField] private Button _spinButton;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private Animator _animator;

    public event UnityAction SpinStarted;
    public event UnityAction SpinEnded;

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(OnButtonClick);
        _pickerWheel.SpinEnded += OnSpinEnded;
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(OnButtonClick);
        _pickerWheel.SpinEnded -= OnSpinEnded;
    }

    private void Start()
    {
        _priceText.text = _price.ToString();
    }

    private void OnButtonClick()
    {
        if (_wallet.Subscriber >= _price)
        {
            _pickerWheel.Spin();

            _wallet.RemoveSubscriber(_price);

            _spinButton.interactable = false;
            _animator.SetBool(AnimatorFortuneController.States.IsSpinning, true);

            SpinStarted?.Invoke();
        }
    } 
    
    private void OnSpinEnded(WheelPiece wheelPiece)
    {
        var type = wheelPiece.Type;
        var amount = wheelPiece.Amount;

        if (type == WheelPieceType.Respect)
            _wallet.AddRespect(amount);
        else if (type == WheelPieceType.Subsciber)
            _wallet.AddSubscriber(amount);
        else if (type == WheelPieceType.Key)
            _keyCounter.AddKeys(amount);

        _spinButton.interactable = true;
        _animator.SetBool(AnimatorFortuneController.States.IsSpinning, false);

        SpinEnded?.Invoke();
    }
}