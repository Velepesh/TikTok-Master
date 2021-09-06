using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeGame : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private GameObject _topPrizePlace;
    [SerializeField] private List<Prize> _prizes;
    [SerializeField] private Prize _topPrize;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Image> _keyImages;
    [SerializeField] private Sprite _unlockKeyIcon;

    readonly private int _prizesNumber = 9;
    readonly private int _keyIconNumber = 3;

    private List<PrizeView> _prizeViews = new List<PrizeView>();

    private void Awake()
    {
        if (_prizes.Count < _prizesNumber)
            new ArgumentNullException();

        if (_keyImages.Count < _keyIconNumber)
            new ArgumentNullException();

        AddTopPrize(_topPrize);

        var mixedPrizes = _prizes.OrderBy(x => Guid.NewGuid()).ToList();

        for (int i = 0; i < _prizesNumber; i++)
        {
            Prize prize = mixedPrizes[i];

            AddItem(prize);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _prizeViews.Count; i++)
            _prizeViews[i].PrizeButtonClick += OnPrizeButtonClick;
    }

    private void AddTopPrize(Prize prize)
    {
        var view = Instantiate(prize.View, _topPrizePlace.transform);

        view.Render(prize);
    }

    private void AddItem(Prize prize)
    {
        var view = Instantiate(prize.View, _itemContainer.transform);
        _prizeViews.Add(view);

        view.PrizeButtonClick += OnPrizeButtonClick;
        view.Render(prize);
    }

    private void OnPrizeButtonClick(Prize prize, PrizeView view)
    {
        int keysNumber = _keyCounter.KeysNumber;

        if (prize.Type == PrizeType.Respect)
            _wallet.AddRespect(prize.Award);
        else if (prize.Type == PrizeType.Subsciber)
            _wallet.AddSubscriber(prize.Award);

        UnlockKey(keysNumber);

        if (keysNumber <= 0)
        {
            _animator.SetTrigger(AnimatorPrizeGameController.States.NextButton);
        }
    }

    private void UnlockKey(int keysNumber)
    {
        _keyImages[keysNumber].sprite = _unlockKeyIcon;
    }
}