using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enemy : Item
{
    [SerializeField] private Vibration _vibration;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            _vibration.Vibrate();
            PlayEffect();

            progress.RemoveProgress(Value);

            if (other.TryGetComponent(out Player player))
            {
                if (player.IsLose == false)
                {
                    player.Stumble();
                    player.Miss();
                }
            }
        }
    }

    private void OnTriggerExit()
    {
        GetComponent<Collider>().enabled = false;
    }
}