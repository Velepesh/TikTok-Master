using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferWalletIcon : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private TMP_Text _awardText;

    private Transform _target;

    private bool _isFollowing;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }

    public void Init(Vector3 startPosition, Transform target, int award)
    {
        transform.position = startPosition;
        _target = target;

        if (award > 0)
            _awardText.text = award.ToString();

        StartFollowing();
    }

    public void InitAwardText(int award)
    {
        _awardText.text = award.ToString();
    }

    private void Update()
    {
        if (_isFollowing)
            Move();
    }

    private void StartFollowing()
    {
        _isFollowing = true;
    }

    private void StopFollowing()
    {
        _isFollowing = false;

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed);

        if (transform.position == _target.position)
            StopFollowing();
    }
}