using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieceView : MonoBehaviour
{
    [SerializeField] public Image _icon;
    [SerializeField] private TMP_Text _text;

    public void AddIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    public void AddText(int amount)
    {
        _text.text = "X " + amount.ToString();
    }
}