using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prize", menuName = "Prize/Prize", order = 51)]
public class Prize : ScriptableObject
{
    [SerializeField] private PrizeView _view; 
    [SerializeField] private int _award; 
    [SerializeField] private PrizeType _type;
    
    private bool _isOpened;

    public PrizeView View => _view;
    public PrizeType Type => _type;
    public int Award => _award;
    public bool IsOpened => _isOpened;
}

public enum PrizeType 
{ 
    Respect,
    Subsciber,
}