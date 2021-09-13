using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelIndexText;
    [SerializeField] private Image _icon;

    public void WriteLevelIndex(int index)
    {
        _levelIndexText.text = index.ToString();
    }

    public void ChangeIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }
}