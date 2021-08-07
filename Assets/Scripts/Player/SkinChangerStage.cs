using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangerStage : MonoBehaviour
{
    readonly private int _nerdValue = 0;
    readonly private int _clerkValue = 25;
    readonly private int _ordinaryValue = 50;
    readonly private int _stylishValue = 75;
    readonly private int _tiktokerValue = 100;

    public int NerdValue => _nerdValue;
    public int ClerkValue => _clerkValue;
    public int OrdinaryValue => _ordinaryValue;
    public int StylishValue => _stylishValue;
    public int TiktokerValue => _tiktokerValue;
}
