using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEnemyShoot : MonoBehaviour
{
    public Transform firePoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shooter;
    
    private float fireRate; // shoots/sec
    private float bulletSpeed;
    private float lastCooldown;

    void Start()
    {
        lastCooldown = Time.time + 1;
        firePoint = GetComponent<Transform>();
        bulletSpeed = bulletPrefab.GetComponent<HackingBullet>().speed;
        fireRate = shooter.GetComponent<HackingEnemy>().fireRate;
    }

    void Update()
    {
        if (fireRate > 0)
            fire(); 
    }

    void fire()
    {
        if (lastCooldown > Time.time)
            return;
        
        lastCooldown = Time.time + (1f / fireRate);

        GameObject bullet = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
