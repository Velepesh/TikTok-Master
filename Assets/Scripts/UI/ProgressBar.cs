using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _fillingTime;

    private float _currentValue;

    private void Update()
    {
        if (_slider.value != _currentValue)
            _slider.value = Mathf.Lerp(_slider.value, _currentValue, _fillingTime * Time.deltaTime);
    }
    private void OnValueChanged(int value, int maxValue)
    {
        _currentValue = (float)value / maxValue;
    }

    private IEnumerator ChangeSliderValue(float currentValue)
    {
        //while (_slider.value != currentValue)
        //{
        //   // float progress = Mathf.Clamp01()
        //   elapsedTime += Time.deltaTime / _fillingTime;

        //   _slider.value = Mathf.Lerp(_slider.value, currentValue, _fillingTime * Time.deltaTime);

        //   yield return null;
        //   Debug.Log("sd");
        //}
        float time = 0;
        float tempAmount = currentValue;
        float diff = tempAmount - tempAmount;

        currentValue = tempAmount;
        while (time < _fillingTime)
        {
            time += Time.deltaTime;
            float percent = time / _fillingTime;
            _slider.value = tempAmount + diff * percent;
            yield return null;
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