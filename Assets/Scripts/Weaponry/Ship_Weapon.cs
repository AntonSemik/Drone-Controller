using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship_Weapon : MonoBehaviour
{
    protected float _isShooting;

    public void OnShoot(InputAction.CallbackContext _context)
    {
        _isShooting = _context.ReadValue<float>();
    }
}
