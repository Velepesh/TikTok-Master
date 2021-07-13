using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IncreasingItem : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.IncreaseTikTokValue(Value);

            GameObject go = Instantiate(DestroyEffect.gameObject, gameObject.transform.position + Offset, Quaternion.identity) as GameObject;

            Destroy(go, Time);
            gameObject.SetActive(false);
        }
    }
}