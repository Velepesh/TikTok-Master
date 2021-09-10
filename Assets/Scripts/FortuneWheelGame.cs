using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FortuneWheelGame : MonoBehaviour
{
    [SerializeField] private PickerWheel _pickerWheel;
    [SerializeField] private Button _spinButton;
    [SerializeField] private TMP_Text _speenButtonText;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private Animator _animator;

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

    private void OnButtonClick()
    {
        _spinButton.interactable = false;
        _speenButtonText.text = "Spining";
        _pickerWheel.Spin();
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

        _animator.SetTrigger(AnimatorFortuneController.States.SpinButton);
    }
}