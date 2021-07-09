using UnityEngine;
using UnityEngine.UI;

public class TikTokBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillArea;
    [SerializeField] private Color _lowColor;
    [SerializeField] private Color _highColor;
    private void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;

        if (_player.HalfTikTokValue > value)
        {
            _fillArea.color = _lowColor;
        }
        else
        {
            _fillArea.color = _highColor;
        }
    }

    private void OnEnable()
    {
        _player.TikTokValueChanged += OnValueChanged;
        _slider.value = 0;
    }

    private void OnDisable()
    {
        _player.TikTokValueChanged -= OnValueChanged;
    }
}