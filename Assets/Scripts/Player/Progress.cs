using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

[RequireComponent(typeof(Player), typeof(Wallet), typeof(SkinChangerStage))]
public class Progress : MonoBehaviour
{
    [SerializeField] private Income _income;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _addAudioClip;
    [SerializeField] private AudioClip _removeAudioClip;

    readonly private int _subscribersMultiplayer = 3;

    private Player _player;
    private Wallet _wallet;
    private SkinChangerStage _stage;
    private int _progression;
    private int _multiplier = 1;
    private int _levelRespect;
    private int _maxProgression;

    public int LevelRespect => _levelRespect;
    public int Progression => _progression;
    public int MaxProgression => _maxProgression;

    public event UnityAction<int, int> ProgressionChanged;
    public event UnityAction<int> AddedProgression;
    public event UnityAction<int> RemovedProgression;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _wallet = GetComponent<Wallet>();
        _stage = GetComponent<SkinChangerStage>();

        AssignStartValue();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    public void AddProgress(int value)
    {
        _progression += value;

        PlayAddClip();
        AddedProgression?.Invoke(value);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void AddSubscribers(int subscribers)
    {
        int progressValue = subscribers * _subscribersMultiplayer;

        AddProgress(progressValue);

        _wallet.AddSubscriber(subscribers, false);
    }

    public void RemoveProgress(int respect)
    {
        _progression -= respect;

        if (_progression < 0)
        {
            _progression = 0;
            _player.Lose();

            return;
        }

        PlayRemoveClip();
        RemovedProgression?.Invoke(respect);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void ApplyMultiplier(int multiplier)
    {
        _multiplier = multiplier;
    }

    public void CollectedLevelRespects()
    {
        _wallet.AddRespect(_levelRespect);
        _wallet.SaveSubscriberData();
    }

    private void PlayAddClip()
    {
        _audioSource.PlayOneShot(_addAudioClip);
    }

    private void PlayRemoveClip()
    {
        _audioSource.PlayOneShot(_removeAudioClip);
    }

    private void AssignStartValue()
    {
        _progression = _stage.ClerkValue;
        _maxProgression = _stage.TiktokerValue;

        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    private void OnWon()//Сделать еще событие
    {
        _levelRespect = _progression * _multiplier + _income.Award;

        if (_levelRespect <= 0)
            _levelRespect = 0;
    }
}