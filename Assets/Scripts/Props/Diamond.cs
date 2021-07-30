using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _time;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private Vector3 _offset;

    private void OnValidate()
    {
        if (_value < 0)
        {
            _value = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            GameObject go = Instantiate(_destroyEffect.gameObject, gameObject.transform.position + _offset, Quaternion.identity) as GameObject;

            Destroy(go, _time);
            gameObject.SetActive(false);
        }
    }
}
