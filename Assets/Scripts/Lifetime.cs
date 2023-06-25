using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] GameObject affectedObject;
    [SerializeField] float lifetime;
    float timer;

    private void Start()
    {
        if(affectedObject == null)
        {
            affectedObject = gameObject;
        }

        timer = lifetime;
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = lifetime;
            affectedObject.SetActive(false);
        }
    }
}
