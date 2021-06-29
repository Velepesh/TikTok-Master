using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
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