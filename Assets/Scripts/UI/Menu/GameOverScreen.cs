using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverScreen : Screen
{
    [SerializeField] private float _delayTime;

    public event UnityAction RestartButtonClick;

    public override void Close()
    {
        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        StartCoroutine(EnableGameOverScreen());
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    private IEnumerator EnableGameOverScreen()
    {
        yield return new WaitForSeconds(_delayTime);

        ScreenHolder.SetActive(true);
    }

    //[SerializeField] private GameObject _lostScreen;
    //[SerializeField] private Player _player;
    //[SerializeField] private float _delayTime;

    //private void Start()
    //{
    //    ToggleLostScreenState(false);
    //}

    //private void OnEnable()
    //{
    //    _player.Losed += OnLosed;
    //}

    //private void OnDisable()
    //{
    //    _player.Losed -= OnLosed;
    //}

    //private void OnLosed()
    //{
    //    StartCoroutine(EnableGameOverScreen());
    //}

    //private void ToggleLostScreenState(bool isLose)
    //{
    //    _lostScreen.SetActive(isLose);
    //}

    //private IEnumerator EnableGameOverScreen()
    //{
    //    yield return new WaitForSeconds(_delayTime);

    //    ToggleLostScreenState(true);
    //}
}