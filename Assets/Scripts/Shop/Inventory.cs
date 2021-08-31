using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 51)]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<Customize> _customizeSkins;

    public void AddSkin(Customize customize)
    {
        _customizeSkins.Add(customize);
    }

    public Customize GetSkin(int skinNumber)
    {
        return _customizeSkins[skinNumber];
    }

    public int GetCountOfSkins()
    {
        return _customizeSkins.Count;
    }
}
