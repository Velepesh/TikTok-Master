using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "WheelPiece", menuName = "WheelPiece/WheelPiece", order = 51)]
public class WheelPiece : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _amount;
    [SerializeField] private float _chance = 100f;
    [SerializeField] private WheelPieceType _type;
    
    private double _weight = 0f;
    public int Amount => _amount;
    public Sprite Icon => _icon;
    public float Chance => _chance;
    public double Weight => _weight;
    public WheelPieceType Type => _type;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 0f, 100f);
        _amount = Mathf.Clamp(_amount, 0, int.MaxValue);
    }

    public void AddWeight(double weight)
    {
        _weight = weight;
    }
}

public enum WheelPieceType
{
    Respect,
    Subsciber,
    Key,
}
