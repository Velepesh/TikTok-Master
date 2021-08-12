using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _smoothedSpeed;
    [SerializeField] private float _time;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothedSpeed);
        transform.position = smoothedPosition;
    }
}
//public class CameraFollow : MonoBehaviour
//{
//    public Transform target;
//    public float smooth = 0.3f;
//    public float distance = 5.0f;
//    public float height = 1.0f;
//    public float Angle = 20;

//    public LayerMask lineOfSightMask = 0;

//    private float yVelocity = 0.0f;
//    private float xVelocity = 0.0f;
//    private float restTime = 0.0f;

//    private void LateUpdate()
//    {
//        if (!target) return;

//        if (restTime != 0.0f)
//            restTime = Mathf.MoveTowards(restTime, 0.0f, Time.deltaTime);

//        // Damp angle from current y-angle towards target y-angle

//        float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.eulerAngles.x + Angle, ref xVelocity, smooth);

//        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);

//        // Look at the target
//        transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

//        var direction = transform.rotation * -Vector3.forward;
//        var targetDistance = AdjustLineOfSight(target.position + new Vector3(0, height, 0), direction);

//        transform.position = target.position + new Vector3(0, height, 0) + direction * targetDistance;
//    }
//    private float AdjustLineOfSight(Vector3 target, Vector3 direction)
//    {
//        RaycastHit hit;

//        if (Physics.Raycast(target, direction, out hit, distance, lineOfSightMask.value))
//            return hit.distance;
//        else
//            return distance;
//    }
//}