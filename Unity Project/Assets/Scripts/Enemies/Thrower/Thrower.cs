using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Enemy2D
{
    public ThrowerMissile missile;

    void Start()
    {
        SetAnimator();
        setTarget();
        attackAction = Attack;
    }

    void Update()
    {
        attackAction();
    }

    public override void Attack()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= 10f)
            {
                missile.Fire();
                animator.SetTrigger("Throw");
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
        else
        {
            target = FindObjectOfType<Controller>().transform;
        }
    }

    protected override void OnDie()
    {

    }

}
