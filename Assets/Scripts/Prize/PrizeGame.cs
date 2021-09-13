using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Sprite _grayKeyIcon;
    [SerializeField] private Sprite _goldKeyIcon;
    [SerializeField] private Button _threeMoreButton;
    [SerializeField] private TMP_Text _priceText;

    readonly private int _prizesNumber = 9;
    readonly private int _keysNumber = 3;
    readonly private int _threeKeysPrice = 1;

    private bool _isThreeMoreGame = false;
    private List<PrizeView> _prizeViews = new List<PrizeView>();

    public event UnityAction<Vector3> PrizeReceived;

    private void Awake()
    {
        if (_prizes.Count < _prizesNumber)
            new ArgumentNullException();

        if (_keyImages.Count < _keysNumber)
            new ArgumentNullException();

        AddTopPrize(_topPrize);

        var mixedPrizes = _prizes.OrderBy(x => Guid.NewGuid()).ToList();

        for (int i = 0; i < _prizesNumber; i++)
        {
            Prize prize = mixedPrizes[i];

            AddItem(prize);
        }
    }

    private void OnEnable()
    {
        _threeMoreButton.onClick.AddListener(OnThreeMoreButtonClick);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _prizeViews.Count; i++)
            _prizeViews[i].PrizeButtonClick += OnPrizeButtonClick;

        _threeMoreButton.onClick.RemoveListener(OnThreeMoreButtonClick);
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

    private void OnThreeMoreButtonClick()
    {
        if(_wallet.Subscriber >= _threeKeysPrice)
        {
            _isThreeMoreGame = true;

            _wallet.RemoveSubscriber(_threeKeysPrice);
            EnableAllKeys();

            _keyCounter.AddKeys(_keysNumber);

            _animator.SetTrigger(AnimatorPrizeGameController.States.OpenThreeMore);
        }
    }

    private void OnPrizeButtonClick(Prize prize, PrizeView view)
    {
        int keysNumber = _keyCounter.KeysNumber;
        PrizeReceived?.Invoke(view.transform.position);

        if (prize.Type == PrizeType.Respect)
            _wallet.AddRespect(prize.Award);
        else if (prize.Type == PrizeType.Subsciber)
            _wallet.AddSubscriber(prize.Award);

        DisableKey(keysNumber);

        if (keysNumber <= 0)
        {
            _priceText.text = _threeKeysPrice.ToString();

            if(_isThreeMoreGame)
                _animator.SetTrigger(AnimatorPrizeGameController.States.ContinueButton);
            else
                _animator.SetTrigger(AnimatorPrizeGameController.States.EndGame);
        }
    }

    private void EnableAllKeys()
    {
        for (int i = 0; i < _keysNumber; i++)
            _keyImages[i].sprite = _goldKeyIcon; 
    }

    private void DisableKey(int keysNumber)
    {
        _keyImages[keysNumber].sprite = _grayKeyIcon;
    }
}