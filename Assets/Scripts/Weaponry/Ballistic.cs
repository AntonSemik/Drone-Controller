using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ballistic : MonoBehaviour
{
    [SerializeField] float _startVelocity;
    [SerializeField] float _acceleration;
    [SerializeField] float _gravity;

    Rigidbody _physBody;

    private void Awake()
    {
        _physBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _physBody.velocity = transform.forward * _startVelocity;
    }

    private void FixedUpdate()
    {
        _physBody.velocity += (transform.forward * _acceleration + Vector3.up * _gravity) * Time.deltaTime;
    }
}
