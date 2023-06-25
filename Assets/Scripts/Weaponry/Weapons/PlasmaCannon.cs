using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCannon : Ship_Weapon, IUsePool, INeedReload
{
    public Transform gunPoint;

    public GameObject projectilePrefab;
    bool isLoaded;


    public float reloadTime;
    float reloadTimer;

    [SerializeField] int poolSize;
    Queue<GameObject> poolQueue = new Queue<GameObject>();


    GameObject tempObj;

    void Start()
    {
        InitialisePool();
    }

    public void Update()
    {
        if (!isLoaded)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0) isLoaded = true;
        }

        if(isShooting >= 0.1f && isLoaded)
        {
            tempObj = GetObjectFromPool();

            tempObj.transform.position = gunPoint.position;
            tempObj.transform.rotation = gunPoint.rotation;

            tempObj.SetActive(true);

            Reload();
        }
    }

    public GameObject GetObjectFromPool()
    {
        tempObj = poolQueue.Dequeue();
        poolQueue.Enqueue(tempObj);

        return tempObj;
    }

    public void InitialisePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            tempObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            poolQueue.Enqueue(tempObj);
            tempObj.SetActive(false);
        }
    }

    public void Reload()
    {
        isLoaded = false;
        reloadTimer = reloadTime;
    }
}
