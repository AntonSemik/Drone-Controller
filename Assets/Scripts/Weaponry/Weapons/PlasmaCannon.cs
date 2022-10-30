using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCannon : Ship_Weapon, IUsePool, INeedReload
{
    public Transform _gunPoint;

    public GameObject _projectile;
    bool _isLoaded;


    public float _reloadTime;
    float _reloadTimer;

    [SerializeField] int _poolSize;
    Queue<GameObject> _poolQueue = new Queue<GameObject>();


    GameObject _tempGO;

    void Start()
    {
        InitialisePool();
    }

    public void Update()
    {
        if (!_isLoaded)
        {
            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer <= 0) _isLoaded = true;
        }

        if(_isShooting >= 0.1f && _isLoaded)
        {
            _tempGO = GetObjectFromPool();

            _tempGO.transform.position = _gunPoint.position;
            _tempGO.transform.rotation = _gunPoint.rotation;

            _tempGO.SetActive(true);

            Reload();
        }
    }

    public GameObject GetObjectFromPool()
    {
        _tempGO = _poolQueue.Dequeue();
        _poolQueue.Enqueue(_tempGO);

        return _tempGO;
    }

    public void InitialisePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _tempGO = Instantiate(_projectile, transform.position, Quaternion.identity);

            _poolQueue.Enqueue(_tempGO);
            _tempGO.SetActive(false);
        }
    }

    public void Reload()
    {
        _isLoaded = false;
        _reloadTimer = _reloadTime;
    }
}
