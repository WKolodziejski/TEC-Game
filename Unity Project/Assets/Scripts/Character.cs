using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public float hp = 3f;
    public float coolDown = 0f;
    public GameObject shield;
    public DamagePopup popup;

    private Action onDie;
    private Action onDamage;
    private float lastCooldown;
    private bool isDead;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void SetOnDamageAction(Action onDamage)
    {
        this.onDamage = onDamage;
    }

    public void TakeDamage(float damage)
    {
        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + coolDown;

            hp -= damage;

            onDamage?.Invoke();

            if (popup != null)
                Instantiate(popup, transform.position, Quaternion.identity, transform).Hit(damage);

            if (hp <= 0)
            {
                Kill();
            }
            else
            {
                if (shield != null)
                    Destroy(Instantiate(shield, transform), 1f);
            }
        }
    }

    public void Kill()
    {
        if (!isDead)
        {
            hp = 0;
            isDead = true;
            onDamage?.Invoke();
            onDie?.Invoke();
            OnDie();
        }
    }

    protected abstract void OnDie();

}
