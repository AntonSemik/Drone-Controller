using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CalculateTorqueRatio : MonoBehaviour
{
#if UNITY_EDITOR

    public ShipProperties shipProperties;

    private void Update()
    {
        TryGetComponent<ShipProperties>(out shipProperties);
        if (shipProperties != null)
        {
            foreach (ShipProperties.Engine engine in shipProperties.engines)
            {
                engine.engineTorqueRatio = Mathf.Round(engine.module.transform.localPosition.x / (Mathf.Sqrt(engine.module.transform.localPosition.x * engine.module.transform.localPosition.x
                    + engine.module.transform.localPosition.z * engine.module.transform.localPosition.z)) * 1000f) / 1000f;
            }
        }
    }
#endif

}
