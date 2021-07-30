using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    readonly private int _maxTikTokValue = 100;
    readonly private float _waitingTime = 1.2f;
    
    private int _currentTikTokValue;
    private Animator _animator;
    public int CurrentTikTokValue => _currentTikTokValue;
    public int HalfTikTokValue => _maxTikTokValue / 2;
    public int MaxTikTokValue => _maxTikTokValue;

    public event UnityAction<int, int> TikTokValueChanged;
    public event UnityAction PlayerHit;
    public event UnityAction PlayerStoped;
    public event UnityAction PlayerRotated;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void ReportOfChangeTikTokValue()
    {
        TikTokValueChanged?.Invoke(_currentTikTokValue, _maxTikTokValue);
    }

    public void ChangeTikTokValue(int value)
    {
        _currentTikTokValue = value;

        if (_currentTikTokValue > _maxTikTokValue)
        {
            _currentTikTokValue = _maxTikTokValue;
        }

        if (_currentTikTokValue < 0)
        {
            _currentTikTokValue = 0;
        }

        ReportOfChangeTikTokValue();
    }

    public void Hit()
    {
        PlayerHit?.Invoke();

        PlayerStoped?.Invoke();
    }

    public void Fall()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Fall);

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitingTime);

        PlayerStoped?.Invoke();
    }

    public void Slide()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Slide);
    }

    public void SelectedWrongCorridor()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Turn);

        PlayerRotated?.Invoke();
        PlayerStoped?.Invoke();
    }

    public void Win()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Dance);

        PlayerRotated?.Invoke();
        PlayerStoped?.Invoke();
    }
}