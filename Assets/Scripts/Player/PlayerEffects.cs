using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerMover))]
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _splashEffect;

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
        _playerMover.PlayerSlidingRun += OnPlayerSlidingRun;
    }

    private void OnDisable()
    {
        _player.PlayerHit -= OnPlayerHit;
        _playerMover.PlayerSlidingRun -= OnPlayerSlidingRun;
    }

    private void OnPlayerHit()
    {
        _hitEffect.Play();
    }

    private void OnPlayerSlidingRun(bool isSliding)
    {
        if (isSliding)
            _hitEffect.Play();
        else
            _hitEffect.Stop();
    }
}
