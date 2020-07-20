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
    private float lastCooldown;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage of " + damage);

        if (lastCooldown <= Time.time)
        {
            lastCooldown = Time.time + coolDown;

            hp -= damage;

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

    public void InstaKill()
    {
        onDie?.Invoke();
        OnDie();
    }

    protected abstract void OnDie();

}
