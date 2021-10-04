using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private GameObject _container;
    [SerializeField] private Sprite _passedIcon;
    [SerializeField] private Sprite _lockedIcon;
    [SerializeField] private Sprite _currentIcon;

    readonly private int _screenLevelNumber = 5;
    
    private int _visibleNumber;

    private void Start()
    {
        UpdateNumber();

        int startIndex = _visibleNumber - _screenLevelNumber + 1;
        
        UpdateLevelIndex(startIndex, _visibleNumber);
    }

    private void UpdateNumber()
    {
        while (_level.ProgressCounter > _visibleNumber)
        {
            _visibleNumber += _screenLevelNumber;
        }
    }
    private void UpdateLevelIndex(int startIndex, int levelNumber)
    {
        for (int i = startIndex; i <= levelNumber; i++)
        {
            AddCell(i);
        }
    }

    private void AddCell(int sceneIndex)
    {
        int number = sceneIndex;

        var view = Instantiate(_levelView, _container.transform);

        if(number == _level.ProgressCounter)
            view.ChangeIcon(_currentIcon);
        else if(number>  _level.ProgressCounter)
            view.ChangeIcon(_lockedIcon);
        else if (number < _level.ProgressCounter)
            view.ChangeIcon(_passedIcon);

        view.WriteLevelIndex(number);
    }
}