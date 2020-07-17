using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float hp = 3f;
    public float coolDown = 0f;
    public GameObject shield;

    private Action onDie;
    private float lastCooldown;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void TakeDamage(float damage)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + coolDown;

            hp -= damage;

            if (hp <= 0)
            {
                onDie?.Invoke();
                OnDie();
            }
            else
            {
                Destroy(Instantiate(shield, transform), 1f);
            }
        }
    }

    protected abstract void OnDie();

}
