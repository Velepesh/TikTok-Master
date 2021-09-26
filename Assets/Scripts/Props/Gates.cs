using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private int _multiplier;
    [SerializeField] private SkinType _skinType;
    [SerializeField] private SkinChangerStage _stage;

    private Animator _animator;
    private int _progressToSkip;

    private void OnValidate()
    {
        _multiplier = Mathf.Clamp(_multiplier, 2, 5);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _progressToSkip = GetProgressToSkip(_skinType);
    }

    private int GetProgressToSkip(SkinType skinType)
    {
        if (skinType == SkinType.Tiktoker)
            return _stage.TiktokerValue;
        else if (skinType == SkinType.Stylish)
            return _stage.StylishValue;
        else if (skinType == SkinType.Ordinary)
            return _stage.OrdinaryValue;
        else if (skinType == SkinType.Clerk)
            return _stage.ClerkValue;
        else if (skinType == SkinType.Nerd)
            return _stage.NerdValue;

        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            if (progress.Progression >= _progressToSkip)
            {
                progress.ApplyMultiplier(_multiplier);

                _animator.SetTrigger(AnimatorGatesController.States.Open);
            }
            else if (other.TryGetComponent(out Player player))
            {
                player.Win();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            if (progress.Progression >= _progressToSkip)
                gameObject.SetActive(false);
        }
    }
}