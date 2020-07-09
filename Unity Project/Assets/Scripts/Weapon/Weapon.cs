using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform barrel;
    public float cooldown = 0.2f;

    private float lastCooldown;
    private float angle;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fire();
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            if (vertical > 0)
            {
                angle = 45;
            }
            else if (vertical < 0)
            {
                angle = -45;
            }
            else
            {
                angle = 0;
            }
        }
        else
        {
            if (vertical > 0)
            {
                angle = 90;
            }
            else if (vertical < 0)
            {
                angle = -90;
            }
            else
            {
                angle = 0;
            }
        }
    }

    void fire()
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + cooldown;
            Instantiate(bullet, barrel.position, barrel.rotation * Quaternion.Euler(0, angle, 0));
        }
    }

}
