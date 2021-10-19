using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFloor : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private int _value;

    private bool _isPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _isPlayer = true;

            StartCoroutine(RemoveProgress(progress));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
            _isPlayer = false;
    }

    private IEnumerator RemoveProgress(Progress progress)
    {
        while (_isPlayer)
        {
            progress.RemoveProgress(_value);

            if (progress.Progression <= 0)
                _isPlayer = false;

            yield return new WaitForSeconds(_time);
        }
    }
}
