using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private SkinType _skinType;
    [SerializeField] private int _value;
    [SerializeField] private float _time;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private Vector3 _offset;

    private void OnValidate()
    {
        _value = Mathf.Clamp(_value, 0, int.MaxValue);
        _time = Mathf.Clamp(_time, 0f, float.MaxValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SkinChanger skinChanger))
        {
            skinChanger.ChangeSkin(_skinType);

            GameObject go = Instantiate(_destroyEffect.gameObject, gameObject.transform.position + _offset, Quaternion.identity) as GameObject;

            Destroy(go, _time);
            gameObject.SetActive(false);
        }
        
        if (other.TryGetComponent(out Player player))
        {
            player.ChangeTikTokValue(_value);
        }
    }
}