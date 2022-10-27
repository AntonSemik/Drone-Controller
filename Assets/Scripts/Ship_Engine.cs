using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Engine : MonoBehaviour, IDamageable
{
    [SerializeField] private float _durabilityMax;
    private float _durabilityCurrent;

    [SerializeField] private AnimationCurve _powerCurve;
    private float _powerLevel = 1; private float _previousLevel = 0;

    [SerializeField] private Vector3 _thrustStrafeVerticalForward;
    [SerializeField] private Vector3 _rotationPitchYawRoll;

    public delegate void OnEngineStateChanged(Vector3 _thrust, Vector3 _rotation);
    public static OnEngineStateChanged onEngineStateChanged;

    public void Start()
    {
        _durabilityCurrent = _durabilityMax;

        onEngineStateChanged?.Invoke(_thrustStrafeVerticalForward, _rotationPitchYawRoll);
    }

    public void TakeDamage(int _damage)
    {
        _durabilityCurrent -= _damage;
        Mathf.Clamp(_durabilityCurrent, 0, _durabilityMax);

        _previousLevel = _powerLevel;
        _powerLevel = _powerCurve.Evaluate(_durabilityCurrent / _durabilityMax);

        onEngineStateChanged?.Invoke(_thrustStrafeVerticalForward * (_powerLevel - _previousLevel), _rotationPitchYawRoll * (_powerLevel - _previousLevel));
    }
}
