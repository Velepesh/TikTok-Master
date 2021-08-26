using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiDisplay : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Increased += OnIncreased;
        _player.Decreased += OnDecreased;
        _player.Missed += OnMissed;
    }

    private void OnDisable()
    {
        _player.Increased -= OnIncreased;
        _player.Decreased -= OnDecreased;
        _player.Missed -= OnMissed;
    }

    private void OnIncreased()
    {
        PlayGoodEmoji();
    }

    private void OnDecreased()
    {
        PlayBadEmoji();
    }

    private void OnMissed()
    {
        PlayMissEmoji();
    }

    private void PlayGoodEmoji()
    {
        _animator.SetTrigger(AnimatorEmojiController.States.Good);
    }

    private void PlayBadEmoji()
    {
        _animator.SetTrigger(AnimatorEmojiController.States.Bad);
    }

    private void PlayMissEmoji()
    {
        _animator.SetTrigger(AnimatorEmojiController.States.Miss);
    }
}