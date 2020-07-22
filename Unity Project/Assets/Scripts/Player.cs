using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Character
{
    public float coolDown = 0f;
    public GameObject shield;
    protected float lastCooldown;

    public float speed = 5f;

    public override void TakeDamage(float damage)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + coolDown;

            hp -= damage;

            if (popup != null)
                Instantiate(popup, transform.position, Quaternion.identity, transform).Hit(damage);

            if (hp <= 0)
            {
                onDie?.Invoke();
                OnDie();
            }
            else
            {
                if (shield != null)
                    Destroy(Instantiate(shield, transform), 1f);
            }
        }
    }

}
