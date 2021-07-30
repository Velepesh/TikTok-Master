using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulldown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.ChangeToFastRun();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.ChangeToStartSpeed();
            playerMover.StopFastRun();
        }
    }
}