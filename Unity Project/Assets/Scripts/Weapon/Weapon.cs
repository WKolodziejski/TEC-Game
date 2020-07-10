using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform barrelFront;
    public Transform barrelUp;
    public Transform barrelDiagonal;
    public GameObject bullet;
    public float cooldown = 0.2f;

    private Transform barrel;
    private float lastCooldown;

    void Start()
    {
        barrel = barrelFront;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fire();
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        /*if (horizontal == 0)
        {
            if (vertical > 0)
                barrel = barrelUp;

            else
                barrel = barrelFront;
        }
        else
        {
            if (vertical > 0)
                barrel = barrelDiagonal;
            else
                barrel = barrelFront;
        }*/

        if (vertical > 0)
        {
            if (horizontal == 0)
                barrel = barrelUp;
            else
                barrel = barrelDiagonal;
        }
        else
            barrel = barrelFront;
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
