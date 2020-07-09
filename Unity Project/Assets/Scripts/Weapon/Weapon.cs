using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform barrel;
    public float cooldown = 0.2f;

    private float lastCooldown;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fire();
        }
    }

    void fire()
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + cooldown;
            Instantiate(bullet, barrel.position, barrel.rotation);
        }
    }

}
