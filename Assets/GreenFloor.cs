using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFloor : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private int _value;
    [SerializeField] private Vibration _vibration;

    private bool _isPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _isPlayer = true;

            StartCoroutine(AddProgress(progress));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _isPlayer = false;
        }
    }

    private IEnumerator AddProgress(Progress progress)
    {
        while (_isPlayer)
        {
            progress.AddProgress(_value);
            _vibration.Vibrate();

            yield return new WaitForSeconds(_time);
        }
    }
}