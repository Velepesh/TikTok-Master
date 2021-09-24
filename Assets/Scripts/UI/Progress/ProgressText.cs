using UnityEngine;
using TMPro;

class ProgressText : MonoBehaviour
{
    [SerializeField] private TMP_Text _overbarText;
    [SerializeField] private TMP_Text _progressText;

    public void AssignName(SkinType type)
    {
        _overbarText.text = type.ToString();
        _progressText.text = type.ToString();
    }

    public void AssignTextColor(Color color)
    {
        _overbarText.color = color;
        _progressText.color = color;
    }
}