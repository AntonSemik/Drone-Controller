using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MortarShell : MonoBehaviour
{
    public float blastRange;
    public int rayCount;
    public float fragmentDispersionRange;
    public float addedGravity;
    public LayerMask whatToHit;
    public float gizmoTime;

    public Rigidbody rb;

    Vector3 detonationPoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }


    private void FixedUpdate()
    {
        rb.velocity -= Vector3.up * addedGravity * Time.fixedDeltaTime;
    }

    void Explode()
    {
        rb.velocity = Vector3.zero;

        detonationPoint = transform.position - rb.velocity.normalized * 5;

        for (int i = 0; i < rayCount; i++)
        {
            RaycastFragment(detonationPoint);
        }

        BlastWave(detonationPoint);

        Destroy(gameObject, 0.1f);
    }

    void RaycastFragment(Vector3 originPoint)
    {
        RaycastHit hit;
        Vector3 dir = Random.insideUnitSphere;

        if (Physics.Raycast(originPoint, dir, out hit, fragmentDispersionRange, whatToHit))
        {
            MortarTarget mortarTarget;
            if (hit.collider.TryGetComponent<MortarTarget>(out mortarTarget))
            {
                mortarTarget.GetHit();
            }

            Debug.DrawLine(originPoint, hit.point, Color.red, gizmoTime, true);
        }
        else
        {
            Debug.DrawRay(originPoint, dir * fragmentDispersionRange, Color.red, gizmoTime, true);
        }
    }

    void BlastWave(Vector3 originPoint)
    {
        StartCoroutine(DrawBlast());

        Collider[] hitObjects = Physics.OverlapSphere(originPoint, blastRange, whatToHit);

        foreach (Collider collider in hitObjects)
        {
            MortarTarget mortarTarget;
            if (collider.TryGetComponent<MortarTarget>(out mortarTarget))
            {
                mortarTarget.Invoke("GetHit", (collider.transform.position - originPoint).magnitude/330);
            }
        }
    }

    bool drawBlastWave;
    float range = 0;
    private void OnDrawGizmos()
    {
        if(drawBlastWave)
        {
            Gizmos.DrawWireSphere(detonationPoint, range);
        }
    }

    IEnumerator DrawBlast()
    {
        drawBlastWave = true;

        while (range < blastRange)
        {
            range += 330 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        drawBlastWave = false;
    }
}
