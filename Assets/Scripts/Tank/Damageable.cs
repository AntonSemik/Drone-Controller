using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Renderer targetRenderer;

    int hitCount;

    public void GetHit()
    {
        hitCount++;
        targetRenderer.material.color = Color.Lerp(Color.white, Color.red, 0.1f * hitCount);
    }
}
