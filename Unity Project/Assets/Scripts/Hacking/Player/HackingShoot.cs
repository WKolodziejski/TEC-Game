using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingShoot : MonoBehaviour 
{

    public GameObject bulletPrefab;
    public float fireRate;
    public float bulletSpeed;

    private float lastCooldown;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            Fire();
    }

    void Fire()
    {
        if (lastCooldown > Time.time)
            return;
        
        lastCooldown = Time.time + (1f / fireRate);

        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, transform.position, transform.rotation);
        
        HackingBullet bullet = bulletGO.GetComponent<HackingBullet>();

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

}