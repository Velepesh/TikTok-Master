using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _waitingTime;
    
    private Vector3 _offset;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        _offset = transform.position - _target.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = _target.transform.position + _offset;
        transform.position = newPosition;
    }

    public void RotateAfterFinish()
    {
        StartCoroutine(Rotate(_waitingTime));
    }

    private IEnumerator Rotate(float duration)
    {
        yield return new WaitForSeconds(duration);

        _animator.SetTrigger(AnimatorCameraController.States.Rotate);
    }
}