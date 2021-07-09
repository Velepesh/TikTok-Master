using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Сorridor : MonoBehaviour
{
    protected abstract void OnTriggerEnter(Collider other);
}