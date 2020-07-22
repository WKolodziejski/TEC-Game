using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    protected Transform barrel;
    protected Transform target;

    public override void TakeDamage(float damage)
    {
        hp -= damage;

        if (popup != null)
            Instantiate(popup, transform.position, Quaternion.identity, transform).Hit(damage);

        if (hp <= 0)
        {
            onDie?.Invoke();
            OnDie();
        }
    }

    protected void setTarget()
    {
        target = FindObjectOfType<Player>().transform;
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }

    //public abstract void Fire();

    /*public void Fire()
    {
        weapon.Fire(barrel);
    }*/
}
