using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public bool launch = false;
    public Transform launchPoint;
    public float launchVelocity = 20f;
    public MortarShell shell;
    public float blastRange;
    public int rayCount;
    public float fragmentDispersionRange;
    public float addedGravity;

    private void Update()
    {
        if (launch)
        {
            Launch();
            launch = false;
        }
    }

    void Launch()
    {
        MortarShell tempShell = Instantiate(shell, launchPoint.position, launchPoint.rotation);
        tempShell.rb.velocity = (tempShell.transform.up).normalized * launchVelocity;
        //tempShell.rb.velocity = (tempShell.transform.up + Random.insideUnitSphere * 0.01f).normalized * launchVelocity;
        tempShell.rayCount = rayCount;
        tempShell.fragmentDispersionRange = fragmentDispersionRange;
        tempShell.addedGravity = addedGravity;
        tempShell.blastRange= blastRange;
    }
}
