using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _target.transform.position + _offset;

        transform.position = newPosition;
    }
}