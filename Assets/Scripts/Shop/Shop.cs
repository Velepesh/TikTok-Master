using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private List<SkinsHolder> _skinsHolders;

    readonly private string ShopData = "ShopData";

    private int CurrentTemplateIndex => PlayerPrefs.GetInt(ShopData, 0);
    private int _templateIndex = 0;
    private List<SkinView> _skinViews = new List<SkinView>();
    private List<Customize> _customizes = new List<Customize>();
    private SkinsHolder _currentHolder;

    readonly private int _unlockPrice = 1;

    public event UnityAction<SkinsHolder> SelectedHolder;
    public event UnityAction ChoosedSkin;
    public event UnityAction Closed;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _shopScreen.CustomizeButtonClick += OnCustomizeButtonClick;
        _shopScreen.CloseButtonClick += OnCloseButtonClick;
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);

        for (int i = 0; i < _skinViews.Count; i++)
        {
            _skinViews[i].SelectedButtonClick -= OnSelectedButtonClick;
        }

        _shopScreen.CustomizeButtonClick -= OnCustomizeButtonClick;
        _shopScreen.CloseButtonClick -= OnCloseButtonClick;
    }

    private void Awake()
    {
        int boughtSkins = 0;

        for (int i = 0; i < _shopInventory.GetCountOfHolders(); i++)
        {
            var holder = _shopInventory.GetSkinsHolder(i);
            AddItem(holder);

            if (holder.IsBuyed)
                boughtSkins++;
            _skinsHolders[i].gameObject.SetActive(false);
        }

        if (boughtSkins <= 1)
            SaveCurrentTemplateIndex(0);

        EnableSelecteView();
        ApplyNewSkinsHolder(CurrentTemplateIndex);

        _unlockPriceText.text = _unlockPrice.ToString();
    }

    public SkinsHolder GetCurrentHolder()
    {
        return _currentHolder;
    }

    private void OnCustomizeButtonClick()
    {
        ChoosedSkin?.Invoke();
    }

    private void OnCloseButtonClick()
    {
        Closed?.Invoke();
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

        ApplyNewSkinsHolder(_templateIndex);
        SaveCurrentTemplateIndex(_templateIndex);
        EnableSelecteView();
        ChoosedSkin?.Invoke();
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

    private void SaveCurrentTemplateIndex(int index)
    {
        PlayerPrefs.SetInt(ShopData, index);
    }

    private void ApplyNewSkinsHolder(int index)
    {
        if (_currentHolder != null)
            _currentHolder.gameObject.SetActive(false);

        _currentHolder = _skinsHolders[index];
        _currentHolder.gameObject.SetActive(true);
        SelectedHolder?.Invoke(_currentHolder);
    }

    private void EnableSelecteView()
    {
        _skinViews[CurrentTemplateIndex].SelecteBackground(_selectedIcon);
    }

    private void DisableSelecteView()
    {
        _skinViews[CurrentTemplateIndex].SelecteBackground(_backgroundIcon);
    }
}