using System.Collections;
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
        StartCoroutine(OpenGameOverScreen());
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    private IEnumerator OpenGameOverScreen()
    {
        yield return new WaitForSeconds(_delayTime);

        ScreenHolder.SetActive(true);
    }
}