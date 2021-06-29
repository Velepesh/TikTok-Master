using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private int _decreasingValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.DecreaseTikTokValue(_decreasingValue);

            gameObject.SetActive(false);
        }
    }
}