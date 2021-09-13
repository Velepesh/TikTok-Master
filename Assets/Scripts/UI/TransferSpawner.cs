using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSpawner : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private KeyTransfer _keyTemplate;
    [SerializeField] private RespectTransfer _respectTemplate;
    [SerializeField] private SubsciberTransfer _subscriberTemplate;
    [SerializeField] private Transform _respectTarget;
    [SerializeField] private Transform _subscriberTarget;
    [SerializeField] private Transform _keyTarget;
    [SerializeField] private PrizeGame _prizeGame;
    [SerializeField] private PickerWheel _pickerWheel;
    [SerializeField] private GameObject _container;

    private float _keysNumber;
    private Vector3 _startPosition;

    private void Awake()
    {
        _keysNumber = _keyCounter.KeysNumber;
        _startPosition = _container.transform.position;
    }

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
        _wallet.SubscriberChanged += OnSubscriberChanged;
        _keyCounter.KeysNumberChanged += OnKeysNumberChanged;
        _prizeGame.PrizeReceived += OnPrizeReceived;
        _pickerWheel.PrizeReceived += OnPrizeReceived;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
        _wallet.SubscriberChanged -= OnSubscriberChanged;
        _keyCounter.KeysNumberChanged -= OnKeysNumberChanged;
        _prizeGame.PrizeReceived -= OnPrizeReceived;
        _pickerWheel.PrizeReceived -= OnPrizeReceived;
    }

    public void InstantiateTemplate(TransferWalletIcon template, Transform target, int award = 0)
    {
        var prefab = Instantiate(template.gameObject, _container.transform);

        var transfer = prefab.GetComponent<TransferWalletIcon>();
        transfer.Init(_startPosition, target, award);
    }

    private void OnKeysNumberChanged(int value)
    {
        if (_keysNumber <= value)
        {
            _keysNumber = value;

            InstantiateTemplate(_keyTemplate, _keyTarget);
        }
    }

    private void OnRespectChanged(int addValue, int currentValue)
    {
        if (_wallet.Respect <= currentValue)
            InstantiateTemplate(_respectTemplate, _respectTarget, addValue);
    }

    private void OnSubscriberChanged(int addValue, int currentValue)
    {
        if (_wallet.Subscriber <= currentValue)
            InstantiateTemplate(_subscriberTemplate, _subscriberTarget, addValue);
    }

    private void OnPrizeReceived(Vector3 startPosition)
    {
        _startPosition = startPosition;
    }
}
