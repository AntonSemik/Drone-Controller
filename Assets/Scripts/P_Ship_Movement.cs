using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class P_Ship_Movement : MonoBehaviour
{
    public bool _isEnabled = false;

    [SerializeField] private Vector3 _thrustStrafeVerticalForward;
    [SerializeField] private Vector3 _torquePitchYawRoll;

    private Vector3 _inputStrafeVerticalForward;
    private Vector3 _inputPitchYawRoll;

    private Rigidbody _physBody;

    private void Awake()
    {
        _physBody = GetComponent<Rigidbody>();

        Ship_Engine.onEngineStateChanged += OnEngineChange;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (_isEnabled)
        {
            ApplyThrust();
            ApplyTorque();
        }
    }

    public void OnEngineChange(Vector3 _thrust, Vector3 _torque)
    {
        _thrustStrafeVerticalForward += _thrust;
        _torquePitchYawRoll += _torque;
    }

    private void ApplyThrust()
    {
        _physBody.AddRelativeForce(new Vector3(_thrustStrafeVerticalForward.x * _inputStrafeVerticalForward.x, _thrustStrafeVerticalForward.y * _inputStrafeVerticalForward.y, _thrustStrafeVerticalForward.z * _inputStrafeVerticalForward.z));
    }

    private void ApplyTorque()
    {
        _physBody.AddRelativeTorque(new Vector3(_torquePitchYawRoll.x * _inputPitchYawRoll.x, _torquePitchYawRoll.y * -_inputPitchYawRoll.y, - _torquePitchYawRoll.z * _inputPitchYawRoll.z));
    }

    public void OnThrustInput(InputAction.CallbackContext _context)
    {
        _inputStrafeVerticalForward = _context.ReadValue<Vector3>();
    }

    public void OnTorqueInput(InputAction.CallbackContext _context)
    {
        _inputPitchYawRoll = _context.ReadValue<Vector3>();
    }

    private void OnDestroy()
    {
        Ship_Engine.onEngineStateChanged -= OnEngineChange;
    }
}
