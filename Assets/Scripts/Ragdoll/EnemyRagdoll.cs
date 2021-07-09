using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyRagdoll : Ragdoll
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.EnemyHit += OnCharacterHit;
    }

    private void OnDisable()
    {
        _enemy.EnemyHit -= OnCharacterHit;
    }
}