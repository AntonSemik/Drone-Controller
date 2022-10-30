using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsePool
{
    public void InitialisePool();

    public GameObject GetObjectFromPool();
}
