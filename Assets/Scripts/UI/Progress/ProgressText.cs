using UnityEngine;
using TMPro;

class ProgressText : MonoBehaviour
{
    [SerializeField] private TMP_Text _overbarText;
    [SerializeField] private TMP_Text _walletScreenText;

    public void AssignName(SkinType type)
    {
        _overbarText.text = type.ToString();
        _walletScreenText.text = type.ToString();
    }

    public void AssignTextColor(Color color)
    {
        _overbarText.color = color;
        _walletScreenText.color = color;
    }
}