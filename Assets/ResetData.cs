using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("RespectData", 0);
            PlayerPrefs.SetInt("SubscriberData", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerPrefs.SetInt("KeyCounter", 0);
        } 
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt("KeyCounter", 3);
        }
    }
}