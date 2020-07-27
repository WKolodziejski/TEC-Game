using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Enemy2D
{

    public ThrowerMissile missile;
    public float throwDistance = 15f;

    void Update()
    {
        LookAtTarget();
        Attack();

        //if (CanAttack())
            transform.position += Vector3.right * movementSpeed * Time.deltaTime * GetTargetMagnitude();
    }

    public override void Attack()
    {
        if (GetTarget() != null)
        {
            if (Vector2.Distance(transform.position, GetTarget().position) <= throwDistance)
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
