using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator _animator;

    public event UnityAction PlayerStumbled;
    public event UnityAction PlayerStoped;
    public event UnityAction PlayerRotated;
    public event UnityAction FinishLineCrossed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Win()
    {
        PlayerRotated?.Invoke();
        PlayerStoped?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Dance);
    }

    public void Lose()
    {
        PlayerStoped?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Defeat);
    }

    public void Stumble()
    {
        PlayerStumbled?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Stumble);
    }

    public void Rejoice()
    {
        PlayerStumbled?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Rejoice);
    }

    public void CrossedFinishLine()
    {
        FinishLineCrossed?.Invoke();
    }
}