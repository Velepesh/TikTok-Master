using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private SkinType _skinType;

    public SkinType SkinType => _skinType;
}