using UnityEngine;
using TMPro;

class ProgressText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.FinishLineCrossed += OnFinishLineCrossed;
    }

    private void OnDisable()
    {
        _player.FinishLineCrossed -= OnFinishLineCrossed;
    }

    public void AssignName(SkinType type)
    {
        _text.text = type.ToString();
    }

    public void AssignTextColor(Color color)
    {
        _text.color = color;
    }

    private void OnFinishLineCrossed()
    {
        gameObject.SetActive(false);
    }
}