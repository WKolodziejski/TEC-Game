using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    public float hp = 3f;
    public DamagePopup popup;

    protected Action onDie;
    private Action onDamage;
    private bool isDead;
    protected Weapon weapon;

    protected void setWeapon()
    {
        weapon = GetComponent<Weapon>();
    }


    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void SetOnDamageAction(Action onDamage)
    {
        this.onDamage = onDamage;
    }

    public abstract void TakeDamage(float damage);
    
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
