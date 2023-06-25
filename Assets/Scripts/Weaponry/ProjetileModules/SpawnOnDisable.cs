using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDisable : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;

    private void Awake()
    {
        objectToSpawn = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        objectToSpawn.SetActive(false);
    }

    private void OnDisable()
    {
        objectToSpawn.transform.position = transform.position;
        objectToSpawn.transform.rotation = transform.rotation;

        objectToSpawn.SetActive(true);
    }
}
