using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _speedEffect;

    private Player _player;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _player.PlayerHit += OnPlayerHit;
        _playerMover.HighSpeedRun += OnHighSpeedRun;
    }

    private void OnDisable()
    {
        _player.PlayerHit -= OnPlayerHit;
        _playerMover.HighSpeedRun -= OnHighSpeedRun;
    }

    private void OnPlayerHit()
    {
        _hitEffect.Play();
    }

    private void OnHighSpeedRun(bool isHighSpeedRun)
    {
        if (isHighSpeedRun)
            PlaySpeedEffect();
        else
            StopSpeedEffect();
    }

    private void PlaySpeedEffect()
    {
        _speedEffect.Play();
    }

    private void StopSpeedEffect()
    {
        _speedEffect.Stop();
    }
}
