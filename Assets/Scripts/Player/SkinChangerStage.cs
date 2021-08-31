using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangerStage : MonoBehaviour
{
    readonly private int _nerdValue = 0;
    readonly private int _clerkValue = 40;
    readonly private int _ordinaryValue = 100;
    readonly private int _stylishValue = 150;
    readonly private int _tiktokerValue = 200;

    public int NerdValue => _nerdValue;
    public int ClerkValue => _clerkValue;
    public int OrdinaryValue => _ordinaryValue;
    public int StylishValue => _stylishValue;
    public int TiktokerValue => _tiktokerValue;
}
