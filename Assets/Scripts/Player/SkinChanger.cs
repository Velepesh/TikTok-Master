using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class SkinChanger : MonoBehaviour
{
    [SerializeField] private List<GameObject> _boots;
    [SerializeField] private List<GameObject> _pants;
    [SerializeField] private List<GameObject> _tops;
    [SerializeField] private List<GameObject> _hairs;
    [SerializeField] private ParticleSystem _changeEffect;

    private Player _player;
    private int _topIndex = 0;
    private int _pantsIndex = 0;
    private int _bootsIndex = 0;
    private int _hairIndex = 0;
    private GameObject _currentTop;
    private GameObject _currentPants;
    private GameObject _currentBoots;
    private GameObject _currentHair;
    private float _previousValue;

    private void Awake()
    {
        _player = GetComponent<Player>();
        
        _previousValue = 0;
        
        Wear();
    }

    private void OnEnable()
    {
        _player.TikTokValueChanged += OnTikTokValueChanged;
    }

    private void OnDisable()
    {
        _player.TikTokValueChanged -= OnTikTokValueChanged;
    }

    private void OnTikTokValueChanged(int currentTikTokValue, int maxTikTokValue)
    {
        int vector = 0;

        if (currentTikTokValue > _previousValue)
            vector = 1;
        else
            vector = -1;

        ChangePlayerSkin(currentTikTokValue, vector);

        _previousValue = currentTikTokValue;
    }

    private void ChangePlayerSkin(int currentValue, int vector)
    {
        if(vector > 0)
        {
            if (currentValue >= (int)SkinType.HAIR && _previousValue < (int)SkinType.HAIR)
            {
                ChangeHair(vector);
            }

            if (currentValue >= (int)SkinType.TOP && _previousValue < (int)SkinType.TOP)
            {
                ChangeTop(vector);
            }

            if (currentValue >= (int)SkinType.PANTS && _previousValue < (int)SkinType.PANTS)
            {
                ChangePants(vector);
            }

            if (currentValue >= (int)SkinType.BOOTS && _previousValue < (int)SkinType.BOOTS)
            {
                ChangeBoots(vector);
            }
        }
        else
        {
            if (currentValue < (int)SkinType.HAIR && _previousValue >= (int)SkinType.HAIR)
            {
                ChangeHair(vector);
            }

            if (currentValue < (int)SkinType.TOP && _previousValue >= (int)SkinType.TOP)
            {
                ChangeTop(vector);
            }

            if (currentValue < (int)SkinType.PANTS && _previousValue >= (int)SkinType.PANTS)
            {
                ChangePants(vector);
            }

            if (currentValue < (int)SkinType.BOOTS && _previousValue >= (int)SkinType.BOOTS)
            {
                ChangeBoots(vector);
            }
        }
    }

    private void Wear()
    {
        _currentTop = _tops[0];
        _currentPants = _pants[0];
        _currentBoots = _boots[0];
        _currentHair = _hairs[0];
    }

    private void ChangeTop(int vector)
    {
        GetNextIndex(vector, ref _topIndex, _tops);

        ChangeSkin(ref _currentTop, _tops[_topIndex]);
    }

    private void ChangePants(int vector)
    {
        GetNextIndex(vector, ref _pantsIndex, _pants);

        ChangeSkin(ref _currentPants, _pants[_pantsIndex]);
    }

    private void ChangeBoots(int vector)
    {
        GetNextIndex(vector, ref _bootsIndex, _boots);

        ChangeSkin(ref _currentBoots, _boots[_bootsIndex]);
    }

    private void ChangeHair(int vector)
    {
        GetNextIndex(vector, ref _hairIndex, _hairs);

        ChangeSkin(ref _currentHair, _hairs[_hairIndex]);
    }

    private void ChangeSkin(ref GameObject currentItem, GameObject newItem)
    {
        var item = newItem;
        currentItem.SetActive(false);

        currentItem = item;
        currentItem.SetActive(true);

        PlaySkinChangeEffect();
    }

    private void PlaySkinChangeEffect()
    {
        _changeEffect.Play();
    }

    private void GetNextIndex(int vector, ref int currentIndex, List<GameObject> items)
    {
        var index = currentIndex + vector;

        currentIndex = index >= items.Count ? 0 : index;

        if (currentIndex < 0)
            currentIndex = index < 0 ? items.Count - 1 : index;
    }
}