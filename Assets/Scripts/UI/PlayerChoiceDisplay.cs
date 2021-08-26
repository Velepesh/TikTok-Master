using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceDisplay : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.RightChoice += OnRightChoice;
        _player.WrongChoice += OnWrongChoice;
    }

    private void OnDisable()
    {
        _player.RightChoice -= OnRightChoice;
        _player.WrongChoice -= OnWrongChoice;
    }

    private void OnWrongChoice()
    {
        PlayBadText();
    }

    private void OnRightChoice()
    {
        PlayGoodText();
    }

    private void PlayBadText()
    {
        _animator.SetTrigger(AnimatorEmojiController.States.Bad);
    }

    private void PlayGoodText()
    {
        _animator.SetTrigger(AnimatorEmojiController.States.Good);
    }
}