using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Engine : MonoBehaviour, IDamageable
{
    [SerializeField] private float durabilityMax;
    private float durabilityCurrent;

    [SerializeField] private AnimationCurve powerCurve;
    private float powerLevel = 1; private float previousLevel = 0;

    [SerializeField] private Vector3 thrustStrafeVerticalForward;
    [SerializeField] private Vector3 rotationPitchYawRoll;

    public delegate void OnEngineStateChanged(Vector3 _thrust, Vector3 _rotation);
    public static OnEngineStateChanged onEngineStateChanged;

    public void Start()
    {
        durabilityCurrent = durabilityMax;

        onEngineStateChanged?.Invoke(thrustStrafeVerticalForward, rotationPitchYawRoll);
    }

    public void TakeDamage(float damage)
    {
        durabilityCurrent -= damage;
        Mathf.Clamp(durabilityCurrent, 0, durabilityMax);

        previousLevel = powerLevel;
        powerLevel = powerCurve.Evaluate(durabilityCurrent / durabilityMax);

        onEngineStateChanged?.Invoke(thrustStrafeVerticalForward * (powerLevel - previousLevel), rotationPitchYawRoll * (powerLevel - previousLevel));
    }
}
