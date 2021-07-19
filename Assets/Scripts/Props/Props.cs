using UnityEngine;

abstract class Props : MonoBehaviour
{
    protected abstract void OnTriggerEnter(Collider other);
}
