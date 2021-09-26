using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private Progress _progress;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private float _time;

    private int _currentProgression;

    private void OnEnable()
    {
        _progress.ProgressionChanged += OnProgressionChanged;
    }

    private void OnDisable()
    {
        _progress.ProgressionChanged -= OnProgressionChanged;
    }

    private void Start()
    {
        _currentProgression = _progress.Progression;

        _progressText.text = _currentProgression.ToString();
    }

    private void OnProgressionChanged(int progression, int maxProgression)
    {
        StartCoroutine(UpdateProgressionScore(_currentProgression, progression));
    }

    private IEnumerator UpdateProgressionScore(int currentValue, int targetValue)
    {
        while (currentValue != targetValue)
        {
            if (targetValue < currentValue)
            {
                int minus = (currentValue - targetValue) / 2;
                currentValue -= minus;

                if (currentValue - targetValue == 1)
                    currentValue--;
            }

            if (targetValue > currentValue)
            {
                int plus = (targetValue - currentValue) / 2;
                currentValue += plus;

                if (targetValue - currentValue == 1)
                    currentValue += 1;
            }

            _progressText.text = currentValue.ToString();

            yield return new WaitForSeconds(_time);
        }

        _currentProgression = targetValue;
    }

}