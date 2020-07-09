using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBossFirePoint : MonoBehaviour
{
    private Transform tr;
    public GameObject bulletPrefab;

    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        bulletSpeed = bulletPrefab.GetComponent<HackingBullet>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        //Debug.Log("Fire! " + gameObject.name);
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, tr.position, tr.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(tr.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
