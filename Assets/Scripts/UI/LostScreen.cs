using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostScreen : MonoBehaviour
{
    [SerializeField] private GameObject _lostScreen;
    [SerializeField] private Player _player;
    [SerializeField] private float _waitingTime;

    private void Start()
    {
        ToggleLostScreenState(false);
    }

    private void OnEnable()
    {
        _player.Lost += OnLost;
    }

    private void OnDisable()
    {
        _player.Lost -= OnLost;
    }
    private void OnLost()
    {
        ToggleLostScreenState(true);
    }

    private void ToggleLostScreenState(bool isLose)
    {
        _lostScreen.SetActive(isLose);
    }

    private IEnumerator d()
    {
        yield return new WaitForSeconds(_waitingTime);
    }
}
