using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship_Weapon : MonoBehaviour
{
    protected float isShooting;

    public void OnShoot(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValue<float>();
    }
}
