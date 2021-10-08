using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private GameObject _keyModel;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _keyCounter.IncreaseCounter();

            _audioSource.PlayOneShot(_audioClip);
            _keyModel.SetActive(false);
        }
    }
}