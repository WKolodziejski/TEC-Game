using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingShoot : MonoBehaviour 
{
    private Transform firePoint;
    private Transform playerTR;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shooter;
    
    private float fireRate; // shoots/sec
    private float bulletSpeed;
    private float lastCooldown;

    void Start()
    {
        fireRate = shooter.GetComponent<HackingPlayer>().fireRate;
        playerTR = shooter.GetComponent<Transform>();

        firePoint = GetComponent<Transform>();
        bulletSpeed = bulletPrefab.GetComponent<HackingBullet>().speed;
    }

    void Update()
    {
        int angle = 0;
        bool shoot = false, shootDiagonal = false;

        if (Input.GetButton("HackingShootHorizontal")) 
        {
            shoot = true;
            if(Input.GetAxisRaw("HackingShootHorizontal") > 0)
                angle += 0;

            else if(Input.GetAxisRaw("HackingShootHorizontal") < 0)
                angle += 180;
        }

        if (Input.GetButton("HackingShootVertical")) 
        {
            if(Input.GetAxisRaw("HackingShootVertical") > 0)
                angle += 90;
                
            else if(Input.GetAxisRaw("HackingShootVertical") < 0)
            {
                if (angle == 0 && shoot)
                    angle += 360;   //compensar o angulo 0
                angle += 270;
            }
            
            if (shoot)
                shootDiagonal = true;
            else
                shoot = true;
        }

        if (shoot) {
            if (shootDiagonal)
            {
                playerTR.rotation = Quaternion.Euler(Vector3.forward * angle / 2);
                Fire();
            }
            else
            {
                playerTR.rotation = Quaternion.Euler(Vector3.forward * angle);
                Fire();
            }
        } 
    }

    void Fire()
    {
        if (lastCooldown > Time.time)
            return;
        
        lastCooldown = Time.time + (1f / fireRate);

        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, 
                                                        new Vector3(firePoint.position.x, 
                                                                    firePoint.position.y, 
                                                                    0), 
                                                        firePoint.rotation);
        
        HackingBullet bullet = bulletGO.GetComponent<HackingBullet>();

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}