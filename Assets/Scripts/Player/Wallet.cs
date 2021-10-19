using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    readonly private string _respectData = "RespectData";
    readonly private string _subscriberData = "SubscriberData";

    private int _respect;
    private int _subscriber;

    public int Respect => PlayerPrefs.GetInt(_respectData, 0);
    public int Subscriber => PlayerPrefs.GetInt(_subscriberData, 0);

    public event UnityAction<int, int> RespectChanged;
    public event UnityAction<int, int> SubscriberChanged;
    public event UnityAction<Customize> SkinBought;

    private void Awake()
    {
        _respect = Respect;
        _subscriber = Subscriber;
    }

    public void BuySkin(Customize customize, int price)
    {
        RemoveRespect(price);

        SkinBought?.Invoke(customize);
    }

    public void AddRespect(int respect)
    {
        _respect += respect;
        SaveRespectData();

        RespectChanged?.Invoke(respect, _respect);
    }

    public void AddSubscriber(int subscriber, bool isSave = true)
    {
        _subscriber += subscriber;
 
        SubscriberChanged?.Invoke(subscriber, _subscriber);

        if(isSave)
            SaveSubscriberData();
    }

    public void RemoveSubscriber(int subscriber)
    {
        _subscriber -= subscriber;

        SubscriberChanged?.Invoke(subscriber, _subscriber);
        SaveSubscriberData();
    }

    private void SaveRespectData()
    {
        PlayerPrefs.SetInt(_respectData, _respect);
    }

    public void SaveSubscriberData()
    {
        PlayerPrefs.SetInt(_subscriberData, _subscriber);
    }

    private void RemoveRespect(int respect)
    {
        _respect -= respect;
        SaveRespectData();

        RespectChanged?.Invoke(respect, _respect);
    }
}