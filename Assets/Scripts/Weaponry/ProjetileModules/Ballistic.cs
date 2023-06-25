using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ballistic : MonoBehaviour
{
    [SerializeField] float startVelocity;
    [SerializeField] float acceleration;
    [SerializeField] float gravity;

    Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        RB.velocity = transform.forward * startVelocity;
    }

    private void FixedUpdate()
    {
        RB.velocity += (transform.forward * acceleration + Vector3.up * gravity) * Time.deltaTime;
    }
}
