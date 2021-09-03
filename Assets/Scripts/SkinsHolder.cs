using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SkinsHolder : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private HolderType _type;
    public List<Skin> GetSkins()
    {
        return _skins;
    }

    public Skin GetSkin(SkinType type)
    {
        var variants = _skins.Where(skin => skin.SkinType == type);

        if (variants.Count() == 0)
            throw new ArgumentOutOfRangeException(nameof(type));

        return variants.First();
    }
}

public enum HolderType
{
    Man_1,
    Man_2,
    Man_3,
    Women_1,
    Women_2,
    Women_3,
}