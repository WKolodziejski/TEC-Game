using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bullet;
    public float cooldown = 0.2f;

    private float lastCooldown;

    public void Fire(Transform barrel)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + cooldown;
            Instantiate(bullet, barrel.position, barrel.rotation);
        }
    }

}
