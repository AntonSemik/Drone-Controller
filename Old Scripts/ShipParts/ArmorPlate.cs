using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPlate : MonoBehaviour
{
    [SerializeField][Tooltip("Thickness in mm")]
    float baseThickness = 100;
    float heatResistance = 0; //For later heat weapons

    public float GetArmorThickness()
    {
        return baseThickness;
    }

    public float GetHeatResistance()
    {
        return heatResistance;
    }
}
