using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryContainer;

    private Animator _animator;
    private bool _isLose;

    public event UnityAction StartedMoving;
    public event UnityAction Stumbled;
    public event UnityAction RightChoice;
    public event UnityAction WrongChoice;
    public event UnityAction FinishLineCrossed;
    public event UnityAction Increased;
    public event UnityAction Decreased;
    public event UnityAction Missed;
    public event UnityAction GameOver;
    public event UnityAction Won;

    public bool IsLose => _isLose;

    private void Start()
    {
        _isLose = false;

        _animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        StartedMoving?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Move);
    }

    public void Win()
    {
        Won?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Dance);
    }

    public void Lose()
    {
        _isLose = true;

        GameOver?.Invoke();
        Decrease();

        _animator.SetTrigger(AnimatorPlayerController.States.Defeat);
    }

    public void Stumble()
    {
        Stumbled?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Stumble);
    }

    public void Rejoice()
    {
        Stumbled?.Invoke();

        _animator.SetTrigger(AnimatorPlayerController.States.Rejoice);
    }

    public void CrossedFinishLine()
    {
        FinishLineCrossed?.Invoke();
    }

    public void MadeWrongChoice()
    {
        WrongChoice?.Invoke();

        Decrease();
    }

    public void MadeRightChoice()
    {
        RightChoice?.Invoke();

        Increase();
    }

    public void Increase()
    {
        Increased?.Invoke();
    }

    public void Decrease()
    {
        Decreased?.Invoke();
    }

    public void Miss()
    {
        Missed?.Invoke();
    }

    private void GetPlayerSkins()
    {
        //for (int i = 0; i < _inventory.GetCountOfWeapon(); i++)
        //{
        //    Gun gun = _inventory.GetGun(i);

        //    if (gun.IsBuyed)
        //    {
        //        Instantiate(gun.Weapon, _inventoryContainer.transform).TryGetComponent(out Weapon weapon);
        //        _weapons.Add(weapon);
        //        weapon.gameObject.SetActive(false);
        //    }
        //}
    }
}