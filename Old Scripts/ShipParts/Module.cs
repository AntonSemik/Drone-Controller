using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField]
    public float maxDurability = 100f;
    public float currentDurability;

    [SerializeField, Range(0f,1f)]
    float effectiveness = 1f;
    [SerializeField]
    AnimationCurve effectivenessCurve;

    public bool isPenetratable;
    public bool isOwnedByPlayer;

    public GameObject destroyVFX;

    private void Start()
    {
        currentDurability = maxDurability;
    }

    private void OnEnable()
    {
        currentDurability = maxDurability;
    }

    public float GetEffectiveness()
    {
        effectiveness = effectivenessCurve.Evaluate(currentDurability / maxDurability);
        return effectiveness;
    }

    public void UpdateDurability()
    {
        Mathf.Clamp(currentDurability, 0, maxDurability);

        if(currentDurability <= 0)
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
        }
    }
}
