using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
