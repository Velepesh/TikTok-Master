using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.SetInt("RespectData", 9550);
            PlayerPrefs.SetInt("SubscriberData", 3533);
        }

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

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("LastPlayedLevel", 7);
            PlayerPrefs.SetInt("LevelProgressData", 7);
        }
    }
}