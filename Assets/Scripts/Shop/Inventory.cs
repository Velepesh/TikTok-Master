using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 51)]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<Customize> _skinsHolders;

    public void Add(Customize customize)
    {
        _skinsHolders.Add(customize);
    }

    public Customize GetByIndex(int skinNumber)
    {
        return _skinsHolders[skinNumber];
    }

    public int GetCount()
    {
        return _skinsHolders.Count;
    }
}