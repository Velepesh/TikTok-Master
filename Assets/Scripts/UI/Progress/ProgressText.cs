using UnityEngine;
using TMPro;

class ProgressText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void AssignName(SkinType type)
    {
        _text.text = type.ToString();
    }

    public void AssignTextColor(Color color)
    {
        _text.color = color;
    }
}