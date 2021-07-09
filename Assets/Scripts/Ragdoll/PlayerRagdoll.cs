using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerRagdoll : Ragdoll
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.PlayerHit += OnCharacterHit;
    }

    private void OnDisable()
    {
        _player.PlayerHit -= OnCharacterHit;
    }
}
