using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Bullet bullet;
    public float fireRate = 1f;

    private Renderer rd;
    private Vector3 lastPosition;
    private float lastCooldown;
    private float relativeSpeed;

    void Awake()
    {
        tag = transform.parent.tag;
        fireRate = 1 / fireRate;

        lastPosition = transform.parent.position;

        rd = transform.parent.GetComponentInChildren<Renderer>();
    }

    void FixedUpdate()
    {
        relativeSpeed = (transform.parent.position.x - lastPosition.x) / Time.deltaTime;
        lastPosition = transform.parent.position;
    }

    public void Fire(Transform barrel)
    {
        if (GameController.isPaused)
            return;

        if (CanFire())
        {
            lastCooldown = Time.time + fireRate;
            Bullet b = Instantiate(bullet, barrel.position, barrel.rotation);
            b.tag += tag;
            b.Fire(relativeSpeed);
        }
    }

    public bool CanFire()
    {
        return (lastCooldown <= Time.time) && rd.IsVisibleFrom(Camera.main);
    }

    public void RandomizeFireRate()
    {
        fireRate += Random.Range(fireRate * -0.1f, fireRate * +0.1f);
    }

}
