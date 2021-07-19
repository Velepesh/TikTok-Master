using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.PlayerHit += OnPlayerHit;
    }

    private void OnDisable()
    {
        _player.PlayerHit -= OnPlayerHit;
    }

    private void OnPlayerHit()
    {
        _hitEffect.Play();
    }
}
