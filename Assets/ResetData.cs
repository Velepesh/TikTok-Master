using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("WalletData", 0);
        }
    }
}