using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Enemy2D
{

    public ThrowerMissile missile;
    public float throwDistance = 15f;

    private bool fired;

    void Update()
    {
        LookAtTarget();
        Attack();
    }

    public override void Attack()
    {
        if (GetTarget() != null)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime * GetTargetMagnitude();

            if (Vector2.Distance(transform.position, GetTarget().position) <= throwDistance)
            {
                fired = true;
                missile.Fire();
                animator.SetTrigger("Throw");
                Destroy(gameObject, 0.5f);
            }
        }
    }

    protected override void OnDie()
    {
        base.OnDie();

        if (!fired)
            missile.Drop();

        Destroy(gameObject, 0.1f);
    }

}
