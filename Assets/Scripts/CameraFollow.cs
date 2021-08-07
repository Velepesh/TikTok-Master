using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Animator))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _smoothedSpeed;
    [SerializeField] private float _waitingTime;

    private Vector3 _offset;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _offset = transform.position - _target.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _target.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, _smoothedSpeed);
        transform.position = smoothedPosition;
    }

    public void RotateAfterPlayerFinish()
    {
        StartCoroutine(Rotate(_waitingTime));
    }

    private IEnumerator Rotate(float duration)
    {
        yield return new WaitForSeconds(duration);

        _animator.SetTrigger(AnimatorCameraController.States.Rotate);
    }
}
//public class CameraFollow : MonoBehaviour
//{

//    public Transform target;

//    public float smooth = 0.3f;
//    public float distance = 5.0f;
//    public float height = 1.0f;
//    public float Angle = 20;


//    public List<Transform> cameraSwitchView;
//    public LayerMask lineOfSightMask = 0;



//    private float yVelocity = 0.0f;
//    private float xVelocity = 0.0f;
//    [HideInInspector]
//    public int Switch;

//    private int gearst = 0;
//    private float thisAngle = -150;
//    private float restTime = 0.0f;


//    private Rigidbody myRigidbody;


//    ////////////////////////////////////////////// TouchMode (Control) ////////////////////////////////////////////////////////////////////


//    private int PLValue = 0;


//    public void CameraSwitch()
//    {
//        Switch++;
//        if (Switch > cameraSwitchView.Count) { Switch = 0; }
//    }


//    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



//    public void RestCar()
//    {

//        if (restTime == 0)
//        {
//            myRigidbody.AddForce(Vector3.up * 500000);
//            myRigidbody.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y, 0));
//            restTime = 2.0f;
//        }

//    }


//    void Start()
//    {

//        myRigidbody = target.GetComponent<Rigidbody>();

//    }



//    void Update()
//    {

//        if (!target) return;



//        if (Input.GetKeyDown(KeyCode.G))
//        {
//            RestCar();
//        }


//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            Application.LoadLevel(Application.loadedLevel);
//        }



//        if (restTime != 0.0f)
//            restTime = Mathf.MoveTowards(restTime, 0.0f, Time.deltaTime);




//        //GetComponent<Camera>().fieldOfView = Mathf.Clamp(carScript.Speed / 10.0f + 60.0f, 60, 90.0f);



//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            Switch++;
//            if (Switch > cameraSwitchView.Count) { Switch = 0; }
//        }


//            // Damp angle from current y-angle towards target y-angle

//            float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x,
//           target.eulerAngles.x + Angle, ref xVelocity, smooth);

//            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
//            target.eulerAngles.y, ref yVelocity, smooth);

//            // Look at the target
//            transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

//            var direction = transform.rotation * -Vector3.forward;
//            var targetDistance = AdjustLineOfSight(target.position + new Vector3(0, height, 0), direction);


//            transform.position = target.position + new Vector3(0, height, 0) + direction * targetDistance;


//    }



//    float AdjustLineOfSight(Vector3 target, Vector3 direction)
//    {


//        RaycastHit hit;

//        if (Physics.Raycast(target, direction, out hit, distance, lineOfSightMask.value))
//            return hit.distance;
//        else
//            return distance;

//    }


//}