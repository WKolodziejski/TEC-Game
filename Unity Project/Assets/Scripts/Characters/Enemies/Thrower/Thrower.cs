using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Enemy2D
{

    public ThrowerMissile missile;

    protected override void InitializeComponents()
    {
        SetEnemySpawner();
    }

    void Update()
    {
        LookAtTarget();
        Attack();
    }

    public override void Attack()
    {
        if (GetTarget() != null)
        {
            if (CanAttack())
            {
                missile.Fire();
                animator.SetTrigger("Throw");
                Destroy(gameObject, 0.5f);
            }
        }
    }

    protected override void OnDie()
    {
        base.OnDie();

        missile.Drop();

        Destroy(gameObject, 0.1f);
    }

}
