using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class P_Ship_Movement : MonoBehaviour
{
    public bool _isEnabled = false;
    [SerializeField] private int _id = 0;

    [SerializeField] private Vector3 _forcesStrafeVerticalForward;
    [SerializeField] private Vector3 _torquePitchYawRoll;

    private Vector3 _inputStrafeVerticalForward;
    private Vector3 _inputPitchYawRoll;

    private Rigidbody _physBody;

    private void Start()
    {
        _physBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isEnabled)
        {
            ApplyForces();
            ApplyTorque();
        }
    }

    private void ApplyForces()
    {
        _physBody.AddRelativeForce(new Vector3(_forcesStrafeVerticalForward.x * _inputStrafeVerticalForward.x, _forcesStrafeVerticalForward.y * _inputStrafeVerticalForward.y, _forcesStrafeVerticalForward.z * _inputStrafeVerticalForward.z));
    }

    private void ApplyTorque()
    {
        _physBody.AddRelativeTorque(new Vector3(_torquePitchYawRoll.x * _inputPitchYawRoll.x, _torquePitchYawRoll.y * -_inputPitchYawRoll.y, _torquePitchYawRoll.z * _inputPitchYawRoll.z));
    }

    public void OnForceInput(InputAction.CallbackContext _context)
    {
        _inputStrafeVerticalForward = _context.ReadValue<Vector3>();
    }

    public void OnTorqueInput(InputAction.CallbackContext _context)
    {
        _inputPitchYawRoll = _context.ReadValue<Vector3>();
    }
}
