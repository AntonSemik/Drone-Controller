using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] GameObject _object;
    [SerializeField] float _lifetime;
    float _timer;

    private void Start()
    {
        if(_object == null)
        {
            _object = gameObject;
        }

        _timer = _lifetime;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer <= 0)
        {
            _timer = _lifetime;
            _object.SetActive(false);
        }
    }
}
