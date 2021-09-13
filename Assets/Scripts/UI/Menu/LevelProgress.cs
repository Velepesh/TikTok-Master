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
    [SerializeField] private Sprite _finalIcon;

    readonly private int _screenLevelNumber = 5;
    
    private int _updateNumber = 5;

    private void Start()
    {
        while(_level.CurrentSceneIndex > _updateNumber)
            _updateNumber *= 2;

        int startIndex = _updateNumber - _screenLevelNumber;
        
        UpdateLevelIndex(startIndex, _updateNumber);
    }

    private void UpdateLevelIndex(int startIndex, int levelNumber)
    {
        for (int i = startIndex; i < levelNumber; i++)
        {
            AddCell(i);
        }
    }

    private void AddCell(int sceneIndex)
    {
        int number = sceneIndex + 1;

        var view = Instantiate(_levelView, _container.transform);

        if (number % _updateNumber == 0)
        {
            view.ChangeIcon(_finalIcon);
        }
        else
        {
            if (sceneIndex <= _level.CurrentSceneIndex)
                view.ChangeIcon(_passedIcon);
            else
                view.ChangeIcon(_lockedIcon);

            view.WriteLevelIndex(number);
        }
    }
}