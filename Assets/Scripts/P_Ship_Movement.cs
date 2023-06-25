using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class P_Ship_Movement : MonoBehaviour
{
    public bool isEnabled = false;

    [SerializeField] private Vector3 thrustStrafeVerticalForward;
    [SerializeField] private Vector3 torquePitchYawRoll;

    private Vector3 inputStrafeVerticalForward;
    private Vector3 inputPitchYawRoll;

    private Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();

        Ship_Engine.onEngineStateChanged += OnEngineChange;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (isEnabled)
        {
            ApplyThrust();
            ApplyTorque();
        }
    }

    public void OnEngineChange(Vector3 thrust, Vector3 torque)
    {
        thrustStrafeVerticalForward += thrust;
        torquePitchYawRoll += torque;
    }

    private void ApplyThrust()
    {
        RB.AddRelativeForce(new Vector3(thrustStrafeVerticalForward.x * inputStrafeVerticalForward.x, thrustStrafeVerticalForward.y * inputStrafeVerticalForward.y, thrustStrafeVerticalForward.z * inputStrafeVerticalForward.z));
    }

    private void ApplyTorque()
    {
        RB.AddRelativeTorque(new Vector3(torquePitchYawRoll.x * inputPitchYawRoll.x, torquePitchYawRoll.y * -inputPitchYawRoll.y, - torquePitchYawRoll.z * inputPitchYawRoll.z));
    }

    public void OnThrustInput(InputAction.CallbackContext context)
    {
        inputStrafeVerticalForward = context.ReadValue<Vector3>();
    }

    public void OnTorqueInput(InputAction.CallbackContext _context)
    {
        inputPitchYawRoll = _context.ReadValue<Vector3>();
    }

    private void OnDestroy()
    {
        Ship_Engine.onEngineStateChanged -= OnEngineChange;
    }
}
