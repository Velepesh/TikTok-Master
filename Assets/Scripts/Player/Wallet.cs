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

    public event UnityAction<int> RespectChanged;
    public event UnityAction<int> SubscriberChanged;
    public event UnityAction<Customize> SkinBought;

    private void Awake()
    {
        _respect = Respect;
        _subscriber = Subscriber;
    }

    public void BuySkin(Customize customize, int price)
    {
        _respect -= price;
        SaveRespectData(_respect);

        RespectChanged?.Invoke(_respect);
        SkinBought?.Invoke(customize);
    }

    public void AddRespect(int respect)
    {
        _respect += respect;
        SaveRespectData(_respect);

        RespectChanged?.Invoke(_respect);
    }

    public void AddSubscriber(int subscriber)
    {
        _subscriber += subscriber;
        SaveSubscriberData(_subscriber);

        SubscriberChanged?.Invoke(_subscriber);
    }

    private void SaveRespectData(int respect)
    {
        PlayerPrefs.SetInt(RespectData, respect);
    }

    private void SaveSubscriberData(int subscriber)
    {
        PlayerPrefs.SetInt(SubscriberData, subscriber);
    }
}