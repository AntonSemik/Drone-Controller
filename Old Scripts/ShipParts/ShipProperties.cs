using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : MonoBehaviour
{
    public float mass;

    [SerializeField]
    public Engine[] engines;

    [SerializeField]
    public ManeuverModule[] maneuverModules;

    [SerializeField]
    public Weapon[] mainShipWeapons;

    [SerializeField]
    public Weapon[] secondaryShipWeapons;

    [System.Serializable]
    public class Engine
    {
        public string engineTag;
        [Tooltip("Forward, sideways, vertical")]
        public Vector3 axisThrust;
        public float backwardForceRatio = 0.75f;
        public float engineTorqueRatio = 0f;
        public Module module;
    }
    [System.Serializable]
    public class ManeuverModule
    {
        public string moduleTag;
        [Tooltip("Axis torques in order: roll, pitch, yaw")]
        public Vector3 axisTorque;
        public Module module;
    }

    [System.Serializable]
    public class Weapon
    {
        public string weaponName;
        public float reloadTime = 1f;
        public GameObject projectile;
        public int poolCount = 15;
        public bool isReady = true;
        public float reloadTimeLeft = 0f;
        public Transform shootPoint;
        public Module module;


        public Queue<Transform> pool = new Queue<Transform>();
    }


    private void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        //Main weapon projectile
        foreach (Weapon weapon in mainShipWeapons)
        {
            for (int i = 0; i < weapon.poolCount; i++)
            {
                GameObject obj = Instantiate(weapon.projectile, transform.position, transform.rotation);
                weapon.pool.Enqueue(obj.transform);
            }
        }

        //Secondary weapon projectile
        foreach (Weapon weapon in secondaryShipWeapons)
        {
            for (int i = 0; i < weapon.poolCount; i++)
            {
                GameObject obj = Instantiate(weapon.projectile, transform.position, transform.rotation);
                weapon.pool.Enqueue(obj.transform);
                obj.SetActive(false);
            }
        }

    }

    private void Update()
    {
        foreach(Weapon weapon in mainShipWeapons)
        {
            if (!weapon.isReady)
            {
                weapon.reloadTimeLeft -= Time.deltaTime;
                if(weapon.reloadTimeLeft <= 0)
                {
                    weapon.isReady = true;
                }
            }
        }

        foreach (Weapon weapon in secondaryShipWeapons)
        {
            if (!weapon.isReady)
            {
                weapon.reloadTimeLeft -= Time.deltaTime;
                if (weapon.reloadTimeLeft <= 0)
                {
                    weapon.isReady = true;
                }
            }
        }

    }

    public void Reload(Weapon weapon)
    {
        weapon.isReady = false;
        weapon.reloadTimeLeft = weapon.reloadTime;
    }
}