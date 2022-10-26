using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement: MonoBehaviour
{
    Rigidbody physBody;

    //Input variables
    float thrustForward1D;
    float thrustVertical1D;
    float thrustSideways1D;
    float roll1D;
    Vector2 pitchYaw2D;
    float isShooting_main, isShooting_secondary;


    //Player ship link
    public ShipProperties shipProperties;

    //Thrust and torque parameters
    Vector3 resultForce;
    float differenceTorque = 0f;
    float rollResult, pitchResult, yawResult;

    float additionalTorque = 0;
    float additionalForce = 0;

    //For shot projectiles enabling
    Transform currentProjectile;


    //Camera stuff
    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualLookAround;
    CinemachinePOV cinemachinePOVLookAround;
    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualLookForward;
    CinemachinePOV cinemachinePOVLookForward;

    bool isFreeCamera = false;

    private void Start()
    {
        cinemachinePOVLookAround = cinemachineVirtualLookAround.GetCinemachineComponent<CinemachinePOV>();
        //cinemachinePOVLookForward = cinemachineVirtualLookForward.GetCinemachineComponent<CinemachinePOV>();


        physBody = GetComponent<Rigidbody>();
        physBody.centerOfMass = transform.position;

        UpdateShipProperties(shipProperties);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void UpdateShipProperties(ShipProperties newProperties)
    {
        physBody.mass = newProperties.mass;
    }

    private void Update()
    {
        if (isShooting_main != 0)
        {
            ShootWeaponArray(shipProperties.mainShipWeapons);
        }

        if (isShooting_secondary != 0)
        {
            ShootWeaponArray(shipProperties.secondaryShipWeapons);
        }

    }

    private void FixedUpdate()
    {
        AlternativeMovement();
    }

    void AlternativeMovement()
    {
        #region thrust
        resultForce = new Vector3(0, 0, 0); differenceTorque = 0;

        if (thrustForward1D != 0 || thrustSideways1D != 0 || thrustVertical1D != 0)
        {
            //Calculate total forward force and difference torque
            foreach (ShipProperties.Engine engine in shipProperties.engines)
            {
                if (thrustForward1D != 0)
                {
                    additionalTorque = engine.engineTorqueRatio * engine.axisThrust.x * engine.module.GetEffectiveness();
                    additionalForce = engine.axisThrust.x * engine.module.GetEffectiveness();
                    if (thrustForward1D < 0)
                    {
                        additionalForce *= engine.backwardForceRatio;
                        additionalTorque *= engine.backwardForceRatio;
                    }
                    resultForce.x += additionalForce;
                    differenceTorque += additionalTorque;
                }

                if (thrustSideways1D != 0)
                {
                    resultForce.y += engine.axisThrust.y * engine.module.GetEffectiveness();
                }

                if (thrustVertical1D != 0)
                {
                    resultForce.z += engine.axisThrust.z * engine.module.GetEffectiveness();
                }
            }

            physBody.AddRelativeForce(Vector3.forward * thrustForward1D * resultForce.x * Time.deltaTime);
            physBody.AddRelativeForce(Vector3.right * thrustSideways1D * resultForce.y * Time.deltaTime);
            physBody.AddRelativeForce(Vector3.up * thrustVertical1D * resultForce.z * Time.deltaTime);

            physBody.AddRelativeTorque(-Vector3.up * thrustForward1D * differenceTorque * Time.deltaTime);
        }
        #endregion

        #region RollPitchYaw

        rollResult = 0; pitchResult = 0; yawResult = 0;

        //Axis torques in order - roll, pitch, yaw
        if (Mathf.Abs(roll1D) > 0.15f || pitchYaw2D.sqrMagnitude > 0.1f)
        {
            foreach (ShipProperties.ManeuverModule module in shipProperties.maneuverModules)
            {
                rollResult += module.axisTorque.x * module.module.GetEffectiveness() * roll1D;
                pitchResult += module.axisTorque.y * module.module.GetEffectiveness() * Mathf.Clamp(-pitchYaw2D.y, -1f, 1f);
                yawResult += module.axisTorque.z * module.module.GetEffectiveness() * Mathf.Clamp(pitchYaw2D.x, -1f, 1f);
            }

            physBody.AddRelativeTorque(Vector3.back * rollResult * Time.deltaTime);

            if (!isFreeCamera)
            {
                physBody.AddRelativeTorque(Vector3.right * pitchResult * Time.deltaTime);
                physBody.AddRelativeTorque(Vector3.up * yawResult * Time.deltaTime);
            }
        }
        #endregion
    }

    void ShootWeaponArray(ShipProperties.Weapon[] weaponArray)
    {
        foreach (ShipProperties.Weapon weapon in weaponArray)
        {
            if (weapon.isReady)
            {
                currentProjectile = weapon.pool.Dequeue();
                currentProjectile.position = weapon.shootPoint.position;
                currentProjectile.rotation = weapon.shootPoint.rotation;
                currentProjectile.gameObject.SetActive(true);
                weapon.pool.Enqueue(currentProjectile);
                shipProperties.Reload(weapon);
            }
        }
    }

    #region Input
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrustForward1D = context.ReadValue<float>();
    }
    public void OnSideways(InputAction.CallbackContext context)
    {
        thrustSideways1D = context.ReadValue<float>();
    }
    public void OnVertical(InputAction.CallbackContext context)
    {
        thrustVertical1D = context.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }
    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw2D = context.ReadValue<Vector2>();
    }

    public void OnMainWeaponShoot(InputAction.CallbackContext context)
    {
        isShooting_main = context.ReadValue<float>();
    }

    public void OnSecondaryWeaponShoot(InputAction.CallbackContext context)
    {
        isShooting_secondary = context.ReadValue<float>();
    }

    public void OnFreeCameraChange(InputAction.CallbackContext context)
    {
        isFreeCamera = !isFreeCamera;

        //if (!isFreeCamera)
        //{
        //    cinemachineVirtualLookAround.m_Priority = 0;
        //}
        //else
        //{
        //    cinemachineVirtualLookAround.m_Priority = 2;
        //}
    }

    #endregion
}

