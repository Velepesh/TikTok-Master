using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Inventory _shopInventory;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Sprite _backgroundIcon;
    [SerializeField] private Sprite _selectedIcon;
    [SerializeField] private TMP_Text _unlockPriceText;

    readonly private string ShopData = "ShopData";

    private int CurrentTemplate => PlayerPrefs.GetInt(ShopData, 0);
    private int _templateIndex = 0;
    private List<SkinView> _skinViews = new List<SkinView>();
    private List<Customize> _customizes = new List<Customize>();

    readonly private int _unlockPrice = 1;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);

        for (int i = 0; i < _skinViews.Count; i++)
        {
            _skinViews[i].SelectedButtonClick -= OnSelectedButtonClick;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _shopInventory.GetCountOfHolders(); i++)
        {
            AddItem(_shopInventory.GetSkinsHolder(i));
        }

        ChangeSelecteView();

        _unlockPriceText.text = _unlockPrice.ToString();
    }

    private void AddItem(Customize customize)
    {
        _customizes.Add(customize);

        var view = Instantiate(_template, _itemContainer.transform);
        _skinViews.Add(view);

        view.SelectedButtonClick += OnSelectedButtonClick;
        view.Render(customize);
    }

    private void OnButtonClick()
    {
        TrySellSkin();
    }

    private void OnSelectedButtonClick(Customize customize, SkinView view)
    {
        DisableSelecteView();

        for (int i = 0; i < _customizes.Count; i++)
            if (_customizes[i] == customize)
                _templateIndex = i;

        SaveCurrentTemplate(_templateIndex);
        ChangeSelecteView();
    }

    private void TrySellSkin()
    {
        if (_unlockPrice <= _wallet.CurrentRespect)
        {
            List<int> indexOfLockSkins = new List<int>();
            int skinIndex = 0;

            for (int i = 0; i < _skinViews.Count; i++)
            {
                if (_customizes[i].IsBuyed == false)
                {
                    indexOfLockSkins.Add(i);
                }
            }

            if (indexOfLockSkins.Count != 0)
            {
                int index = Random.Range(0, indexOfLockSkins.Count - 1);

                skinIndex = indexOfLockSkins[index];

                _wallet.BuySkin(_customizes[skinIndex], _unlockPrice);
                _customizes[skinIndex].Buy();
                _skinViews[skinIndex].UnlockeView(_customizes[skinIndex]);
            }
        }
    }

    private void SaveCurrentTemplate(int index)
    {
        PlayerPrefs.SetInt(ShopData, index);
    }

    private void ChangeSelecteView()
    {
        _skinViews[CurrentTemplate].SelecteBackground(_selectedIcon);
    }

    private void DisableSelecteView()
    {
        _skinViews[CurrentTemplate].SelecteBackground(_backgroundIcon);
    }
}