using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    readonly private string RespectData = "RespectData";
    readonly private string SubscriberData = "SubscriberData";

    private int _respect;
    private int _subscriber;

    public int Respect => PlayerPrefs.GetInt(RespectData, 0);
    public int Subscriber => PlayerPrefs.GetInt(SubscriberData, 0);

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
        _respect -= price;
        SaveRespectData();

        RespectChanged?.Invoke(price, _respect);
        SkinBought?.Invoke(customize);
    }

    public void AddRespect(int respect)
    {
        _respect += respect;
        SaveRespectData();

        RespectChanged?.Invoke(respect, _respect);
    }

    public void RemoveRespect(int respect)
    {
        _respect -= respect;
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
        PlayerPrefs.SetInt(RespectData, _respect);
    }

    public void SaveSubscriberData()
    {
        PlayerPrefs.SetInt(SubscriberData, _subscriber);
    }
}