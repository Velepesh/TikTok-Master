using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class WalletDisplay : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;
    [SerializeField] protected Wallet Wallet;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected float DelayTime;

    protected int CurentValue;
    protected float StartTime;
    protected bool IsChanged;
    private void Awake()
    {
        StartTime = DelayTime;
    }

    private void Update()
    {
        Timer();
    }

    protected void ChangeValue(int respect, char sign)
    {
        CurentValue += respect;
        IsChanged = true;
        DelayTime = StartTime;

        Text.text = sign + CurentValue.ToString();

        Animator.SetTrigger(AnimatorWalletDisplayController.States.Play);
    }

    private void Timer()
    {
        if (IsChanged)
        {
            DelayTime -= Time.deltaTime;

            if (DelayTime <= 0f)
            {
                CurentValue = 0;
                IsChanged = false;
            }
        }
    }
}