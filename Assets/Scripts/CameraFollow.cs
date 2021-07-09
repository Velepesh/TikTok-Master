using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;

    private Vector3 _velocity = Vector3.zero;
    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }

    private void LateUpdate()
    {
        // transform.LookAt(_target.transform.position);

        Vector3 newPosition = _target.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, _smoothSpeed);
        transform.position = smoothedPosition;
        //Vector3 newPosition = _target.transform.position + _offset;
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, _smoothSpeed);
        //transform.position = smoothedPosition;
    }
}