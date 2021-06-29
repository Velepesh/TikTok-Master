using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _tikTokValue;
    
    private int _currentTikTokValue;

    public event UnityAction<int, int> TikTokValueChanged;

    private void ReportOfChangeTikTokValue()
    {
        TikTokValueChanged?.Invoke(_currentTikTokValue, _tikTokValue);
    }

    public void DecreaseTikTokValue(int value)
    {
        _currentTikTokValue -= value;
        ReportOfChangeTikTokValue();

        if (_currentTikTokValue <= 0)
        {
            _currentTikTokValue = 0;
            //Lose
        }
    }

    public void IncreaseTikTokValue(int value)
    {
        _currentTikTokValue += value;
        ReportOfChangeTikTokValue();

        if (_currentTikTokValue >= _tikTokValue)
        {
            _currentTikTokValue = _tikTokValue;
        }
    }
}
