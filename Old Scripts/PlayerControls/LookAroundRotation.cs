using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAroundRotation : MonoBehaviour
{
    public float horizontalRotateSpeed = 10f;
    public float verticalRotateSpeed = 7f;

    Vector2 pitchYaw2D;

    bool isFreeCamera = false;

    [SerializeField]
    Transform orientation;

    private void FixedUpdate()
    {
        if (isFreeCamera)
        {
            RotateTargetOrigin();
        } else
        {
            ResetTarget();
        }

    }

    void RotateTargetOrigin()
    {
        transform.Rotate(orientation.up, horizontalRotateSpeed * pitchYaw2D.x * Time.deltaTime);
        transform.Rotate(-orientation.right, verticalRotateSpeed * pitchYaw2D.y * Time.deltaTime);

    }

    void ResetTarget()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnFreeCameraChange(InputAction.CallbackContext context)
    {
        isFreeCamera = !isFreeCamera;
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw2D = context.ReadValue<Vector2>();
    }
}
