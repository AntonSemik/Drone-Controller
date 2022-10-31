using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDisable : MonoBehaviour
{
    [SerializeField] GameObject _objectToSpawn;

    private void Awake()
    {
        _objectToSpawn = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        _objectToSpawn.SetActive(false);
    }

    private void OnDisable()
    {
        _objectToSpawn.transform.position = transform.position;
        _objectToSpawn.transform.rotation = transform.rotation;

        _objectToSpawn.SetActive(true);
    }
}
