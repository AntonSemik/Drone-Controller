using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool ownedByPlayer = false;

    public float armorPenetration = 0;
    public float leftoverPenetration;
    public float velocity;
    public float spoilThreshold; //Threshold at which projectile is destroyed into spoil pieces
    public GameObject spoil;
    public float spread;

    public float projectile_lifetime;
    float projectile_lifetimeLeft;

    [SerializeField]
    Rigidbody physbody;

    Module module;
    ArmorPlate armorPlate;

    private void Start()
    {
        physbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(projectile_lifetimeLeft > 0)
        {
            projectile_lifetimeLeft -= Time.deltaTime;
        } else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ResetProperties();
    }

    private void ResetProperties()
    {
        armorPlate = null;
        module = null;
        transform.Rotate(Random.Range(-spread,spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
        physbody.velocity = transform.forward * velocity;
        leftoverPenetration = armorPenetration;
        projectile_lifetimeLeft = projectile_lifetime;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Module>(out module);
        other.TryGetComponent<ArmorPlate>(out armorPlate);


        if (module != null)
        {
            if (ownedByPlayer != module.isOwnedByPlayer)
            {

                Debug.Log(name + " hit module: " + other.name);

                module.currentDurability -= leftoverPenetration;
                module.UpdateDurability();
                //if(module.currentDurability <= 0)
                //{
                //    module.isPenetratable = true;
                //}

                //if (!module.isPenetratable)
                //{
                //    DestroyProjectile();
                //}
            }
        }

        if (armorPlate != null)
        {
            float angle = Vector3.Angle(physbody.velocity, -other.transform.forward);

            float totalArmorThickness = Mathf.Abs(armorPlate.GetArmorThickness() / Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * angle)));
            leftoverPenetration -= totalArmorThickness;
            Debug.Log(name + " hit " + other.name + "; Total thickness = " + totalArmorThickness + "; Angle = " + angle);

            if (leftoverPenetration <= 0)
            {
                Debug.Log(name + " not penetrated " + other.name);

                DestroyProjectile();
            }
            else
            {
                Debug.Log(name + " penetrated " + other.name);
                if (totalArmorThickness > spoilThreshold)
                {
                    int spoilAmount = Mathf.CeilToInt(leftoverPenetration * 0.2f + totalArmorThickness * 0.1f);
                    for (int i = 0; i < spoilAmount; i++)
                    {
                        Spoil();
                    }
                }
            }
        }

    }



    void Spoil()
    {
        Instantiate(spoil, transform.position, transform.rotation);
    }

    void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
