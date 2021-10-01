using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dancer : Item
{
    [SerializeField] private Image _subsvriberImage;
    [SerializeField] private Vibration _vibration;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            progress.AddSubscribers(Value);

            if (other.TryGetComponent(out Player player))
            {
                player.Rejoice();
                player.Increase();
            }

            _subsvriberImage.gameObject.SetActive(false);

            _vibration.Vibrate();
            PlayEffect();
        }
    }

    private void OnTriggerExit()
    {
        GetComponent<Collider>().enabled = false;
    }
}