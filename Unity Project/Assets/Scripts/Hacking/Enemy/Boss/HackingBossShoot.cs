using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBossShoot : MonoBehaviour
{
    private GameObject[] firePointsGO;
    private HackingBossFirePoint[] firePoints;

    private float fireRate;
    private float lastCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        fireRate = GetComponent<HackingBoss>().fireRate;

        firePointsGO = GameObject.FindGameObjectsWithTag("BossFirePoint");
        firePoints = new HackingBossFirePoint[firePointsGO.Length];

        int i = 0;
        foreach (GameObject firePoint in firePointsGO)
        {
            firePoints[i] = firePoint.GetComponent<HackingBossFirePoint>();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate > 0)
            Shoot();
    }

    private void Shoot()
    {
        if (lastCooldown > Time.time)
            return;

        lastCooldown = Time.time + (1f / fireRate);

        foreach (HackingBossFirePoint firePoint in firePoints)
        {
            firePoint.Fire();
        }
    }
}
