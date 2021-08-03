using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangerStage : MonoBehaviour
{
    readonly private int _fifthValue = 100;
    readonly private int _fourthValue = 75;
    readonly private int _thirdValue = 50;
    readonly private int _secondValue = 25;
    readonly private int _firstStage = 0;

    public int FifthValue => _fifthValue;
    public int FourthValue => _fourthValue;
    public int ThirdValue => _thirdValue;
    public int SecondValue => _secondValue;
    public int FirstStage => _firstStage;
}
