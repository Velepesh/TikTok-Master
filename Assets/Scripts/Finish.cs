using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
   [SerializeField] private Collider _trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _trigger.enabled = false;

            player.Win();
        }
    }
}