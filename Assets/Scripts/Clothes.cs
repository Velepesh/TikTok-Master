using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    [SerializeField] private int _increasingValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.IncreaseTikTokValue(_increasingValue);

            gameObject.SetActive(false);
        }
    }
}