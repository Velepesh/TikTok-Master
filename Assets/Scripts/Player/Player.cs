using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    readonly private int _maxTikTokValue = 100;
    
    private int _currentTikTokValue;
    private Animator _animator;
    public int CurrentTikTokValue => _currentTikTokValue;
    public int HalfTikTokValue => _maxTikTokValue / 2;

    public event UnityAction<int, int> TikTokValueChanged;
    public event UnityAction PlayerHit;
    public event UnityAction PlayerStoped;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void ReportOfChangeTikTokValue()
    {
        TikTokValueChanged?.Invoke(_currentTikTokValue, _maxTikTokValue);
    }

    public void IncreaseTikTokValue(int value)
    {
        _currentTikTokValue += value;
        ReportOfChangeTikTokValue();

        if (_currentTikTokValue >= _maxTikTokValue)
        {
            _currentTikTokValue = _maxTikTokValue;
        }
    }

    public void DecreaseTikTokValue(int value)
    {
        _currentTikTokValue -= value;
        ReportOfChangeTikTokValue();

        if (_currentTikTokValue <= 0)
        {
            _currentTikTokValue = 0;
        }
    }

    public void Hit()
    {
        PlayerHit?.Invoke();

        PlayerStoped?.Invoke();
    } 
    
    public void SelectedWrongCorridor()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Turn);

        PlayerStoped?.Invoke();
    }

    public void Win()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Dance);

        PlayerStoped?.Invoke();
    }
}