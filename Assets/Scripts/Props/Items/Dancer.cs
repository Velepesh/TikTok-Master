using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dancer : Item
{
    [SerializeField] private Image _subsvriberImage;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            if (other.TryGetComponent(out Player player))
            {
                player.Rejoice();
                player.Increase();
                progress.AddSubscribers(Value);
            }

            _subsvriberImage.gameObject.SetActive(false);

            PlayEffect();

            GetComponent<Collider>().enabled = false;
        }
    }
}