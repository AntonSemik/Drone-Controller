using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    GameObject[] mainLasers;
    [SerializeField]
    GameObject[] secondaryLasers;

    float isShooting_main;
    float isShooting_secondary;

    private void Update()
    {
        CheckIfShooting(isShooting_main, mainLasers);
        CheckIfShooting(isShooting_secondary, secondaryLasers);

    }

    void CheckIfShooting(float input, GameObject[] lasers)
    {
        if(input > 0.1f)
        {
            ToggleLasers(lasers, true);
        } else
        {
            ToggleLasers(lasers, false);

        }
    }

    void ToggleLasers(GameObject[] lasers, bool toggleTo)
    {
        foreach(GameObject laser in lasers)
        {
            laser.SetActive(toggleTo);
        }
    }

    public void OnMainWeaponShoot(InputAction.CallbackContext context)
    {
        isShooting_main = context.ReadValue<float>();
    }

    public void OnSecondaryWeaponShoot(InputAction.CallbackContext context)
    {
        isShooting_secondary = context.ReadValue<float>();
    }

}
